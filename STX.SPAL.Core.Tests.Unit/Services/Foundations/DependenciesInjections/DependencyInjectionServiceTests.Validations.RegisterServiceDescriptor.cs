// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
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
        [MemberData(nameof(RegisterServiceDescriptorValidationExceptions))]
        private void ShouldThrowValidationExceptionIfInvalidParameters(
            DependencyInjection someDependencyInjection,
            Type spalInterfaceType,
            Type implementationType,
            Xeption exception)
        {
            // given
            var expectedServiceCollectionValidationException =
                new DependencyInjectionValidationException(
                    message: "Dependency Injection validation error occurred, fix errors and try again.",
                    innerException: exception);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.AddServiceDescriptor(
                        It.IsAny<IServiceCollection>(),
                        It.IsAny<ServiceDescriptor>()));

            // when
            Func<DependencyInjection> registerServiceDescriptorFunction = () =>
                this.dependencyInjectionService.RegisterServiceDescriptor(
                    someDependencyInjection,
                    spalInterfaceType,
                    implementationType,
                    ServiceLifetime.Singleton);

            DependencyInjectionValidationException actualServiceCollectionValidationException =
                Assert.Throws<DependencyInjectionValidationException>(
                    registerServiceDescriptorFunction);

            //then
            actualServiceCollectionValidationException.Should().BeEquivalentTo(
                expectedServiceCollectionValidationException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.AddServiceDescriptor(
                        It.IsAny<IServiceCollection>(),
                        It.IsAny<ServiceDescriptor>()),
                Times.Never);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(RegisterServiceDescriptorWithSpalIdValidationExceptions))]
        private void ShouldThrowValidationExceptionIfInvalidParametersWhenUsingSpalId(
            DependencyInjection someDependencyInjection,
            Type spalInterfaceType,
            string spalId,
            Type implementationType,
            Xeption exception)
        {
            // given
            var expectedServiceCollectionValidationException =
                new DependencyInjectionValidationException(
                    message: "Dependency Injection validation error occurred, fix errors and try again.",
                    innerException: exception);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.AddServiceDescriptor(
                        It.IsAny<IServiceCollection>(),
                        It.IsAny<ServiceDescriptor>()));

            // when
            Func<DependencyInjection> registerServiceDescriptorFunction = () =>
                this.dependencyInjectionService.RegisterServiceDescriptor(
                    someDependencyInjection,
                    spalInterfaceType,
                    spalId,
                    implementationType,
                    ServiceLifetime.Singleton);

            DependencyInjectionValidationException actualServiceCollectionValidationException =
                Assert.Throws<DependencyInjectionValidationException>(
                    registerServiceDescriptorFunction);

            //then
            actualServiceCollectionValidationException.Should().BeEquivalentTo(
                expectedServiceCollectionValidationException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.AddServiceDescriptor(
                        It.IsAny<IServiceCollection>(),
                        It.IsAny<ServiceDescriptor>()),
                Times.Never);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }
    }
}
