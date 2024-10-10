// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using STX.SPAL.Abstractions;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections.Exceptions;
using Xeptions;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.DependenciesInjections
{
    public partial class DependencyInjectionServiceTests
    {
        [Theory]
        [MemberData(nameof(GetServiceValidationExceptions))]
        private async Task ShouldThrowValidationExceptionIfInvalidParametersOnGetService(
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
                    broker.GetServiceAsync<ISPALBase>(
                        It.IsAny<IServiceProvider>()));

            // when
            Func<Task<ISPALBase>> getServiceFunction =
                () =>
                    this.dependencyInjectionService.GetServiceAsync<ISPALBase>(
                        someDependencyInjection)
                    .AsTask();

            DependencyInjectionValidationException actualDependencyInjectionValidationException =
                await Assert.ThrowsAsync<DependencyInjectionValidationException>(
                    getServiceFunction);

            // then
            actualDependencyInjectionValidationException.Should().BeEquivalentTo(
                expectedDependencyInjectionValidationException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.GetServiceAsync<ISPALBase>(
                        It.IsAny<IServiceProvider>()),
                Times.Never);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(GetServiceWithSpalValidationExceptions))]
        private async Task ShouldThrowValidationExceptionIfInvalidParametersOnGetServiceWithSpal(
            DependencyInjection someDependencyInjection,
            string someSpalId,
            Xeption exception)
        {
            // given
            var expectedDependencyInjectionValidationException =
                new DependencyInjectionValidationException(
                    message: "Dependency Injection validation error occurred, fix errors and try again.",
                    innerException: exception);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.GetServiceAsync<ISPALBase>(
                        It.IsAny<IServiceProvider>(),
                        It.IsAny<string>()));

            // when
            Func<Task<ISPALBase>> getServiceFunction =
                () =>
                    this.dependencyInjectionService.GetServiceAsync<ISPALBase>(
                        someDependencyInjection,
                        someSpalId)
                    .AsTask();

            DependencyInjectionValidationException actualDependencyInjectionValidationException =
                await Assert.ThrowsAsync<DependencyInjectionValidationException>(
                    getServiceFunction);

            // then
            actualDependencyInjectionValidationException.Should().BeEquivalentTo(
                expectedDependencyInjectionValidationException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.GetServiceAsync<ISPALBase>(
                        It.IsAny<IServiceProvider>(),
                        It.IsAny<string>()),
                Times.Never);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }
    }
}
