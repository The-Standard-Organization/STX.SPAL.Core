// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections.Exceptions;
using Xeptions;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.DependenciesInjections
{
    public partial class DependencyInjectionServiceTests
    {
        [Theory]
        [MemberData(nameof(BuildServiceProviderValidationExceptions))]
        private async Task ShouldThrowValidationExceptionIfInvalidParametersOnBuildServiceProvider(
            DependencyInjection someDependencyInjection,
            Xeption exception)
        {
            // given
            var expectedDependencyInjectionValidationException =
                new DependencyInjectionValidationException(
                    message: "Dependency Injection validation error occurred, fix errors and try again.",
                    innerException: exception);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.BuildServiceProviderAsync(
                        It.IsAny<IServiceCollection>()));

            // when
            Func<Task<DependencyInjection>> buildServiceProviderFunction =
                () =>
                    this.dependencyInjectionService.BuildServiceProviderAsync(
                        someDependencyInjection)
                    .AsTask();

            DependencyInjectionValidationException actualDependencyInjectionValidationException =
                await Assert.ThrowsAsync<DependencyInjectionValidationException>(
                    buildServiceProviderFunction);

            // then
            actualDependencyInjectionValidationException.Should().BeEquivalentTo(
                expectedDependencyInjectionValidationException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.BuildServiceProviderAsync(
                        It.IsAny<IServiceCollection>()),
                Times.Never);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }
    }
}
