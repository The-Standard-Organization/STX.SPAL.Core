// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using STX.SPAL.Core.Models.Services.Foundations.ServicesCollections.Exceptions;
using Xeptions;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.ServicesCollections
{
    public partial class ServiceCollectionServiceTests
    {
        [Theory]
        [MemberData(nameof(RegisterServiceDescriptorValidationExceptions))]
        private void ShouldThrowValidationExceptionIfInvalidParameters(
            Type spalInterfaceType,
            Type implementationType,
            Xeption exception)
        {
            // given
            var expectedServiceCollectionValidationException =
                new ServiceCollectionValidationException(
                    message: "Service Collection validation error occurred, fix errors and try again.",
                    innerException: exception);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.AddServiceDescriptor(
                        It.IsAny<ServiceDescriptor>()));

            // when
            Func<IServiceCollection> registerServiceDescriptorFunction = () =>
                this.serviceCollectionService.RegisterServiceDescriptor(
                    spalInterfaceType,
                    implementationType,
                    ServiceLifetime.Singleton);

            ServiceCollectionValidationException actualServiceCollectionValidationException =
                Assert.Throws<ServiceCollectionValidationException>(
                    registerServiceDescriptorFunction);

            //then
            actualServiceCollectionValidationException.Should().BeEquivalentTo(
                expectedServiceCollectionValidationException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.AddServiceDescriptor(It.IsAny<ServiceDescriptor>()),
                Times.Never);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(RegisterServiceDescriptorWithSpalIdValidationExceptions))]
        private void ShouldThrowValidationExceptionIfInvalidParametersWhenUsingSpalId(
            Type spalInterfaceType,
            string spalId,
            Type implementationType,
            Xeption exception)
        {
            // given
            var expectedServiceCollectionValidationException =
                new ServiceCollectionValidationException(
                    message: "Service Collection validation error occurred, fix errors and try again.",
                    innerException: exception);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.AddServiceDescriptor(
                        It.IsAny<ServiceDescriptor>()));

            // when
            Func<IServiceCollection> registerServiceDescriptorFunction = () =>
                this.serviceCollectionService.RegisterServiceDescriptor(
                    spalInterfaceType,
                    spalId,
                    implementationType,
                    ServiceLifetime.Singleton);

            ServiceCollectionValidationException actualServiceCollectionValidationException =
                Assert.Throws<ServiceCollectionValidationException>(
                    registerServiceDescriptorFunction);

            //then
            actualServiceCollectionValidationException.Should().BeEquivalentTo(
                expectedServiceCollectionValidationException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.AddServiceDescriptor(It.IsAny<ServiceDescriptor>()),
                Times.Never);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }
    }
}
