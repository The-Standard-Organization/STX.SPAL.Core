// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using STX.SPAL.Core.Models.Services.Foundations.ServicesCollections.Exceptions;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.ServicesCollections
{
    public partial class ServiceCollectionServiceTests
    {
        [Theory]
        [MemberData(nameof(RegisterServiceDescriptorValidationDependencyExceptions))]
        private void ShouldThrowValidationDependencyExceptionOnRegisterServiceDescriptorIfExternalExceptionOccurs(
            Exception externalException)
        {
            // given
            dynamic randomProperties = CreateRandomProperties();
            ServiceDescriptor someServiceDescriptor = randomProperties.ServiceDescriptor;

            var addServiceDescriptorException =
                new AddServiceDescriptorException(
                    message: "Add service descriptor error occurred, contact support.",
                    innerException: externalException);

            var expectedServiceCollectionValidationDependencyException =
                new ServiceCollectionValidationDependencyException(
                    message: "Service collection validation dependency error occurred, contact support.",
                    innerException: addServiceDescriptorException);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.AddServiceDescriptor(
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            actualServiceDescriptor == someServiceDescriptor)))
                .Throws(externalException);

            // when
            Func<IServiceCollection> registerServiceDescriptorFunction = () =>
                this.serviceCollectionService.RegisterServiceDescriptor(
                    randomProperties.SpalInterfaceType,
                    randomProperties.ImplementationType,
                    randomProperties.ServiceLifeTime);

            ServiceCollectionValidationDependencyException actualServiceCollectionValidationDependencyException =
                Assert.Throws<ServiceCollectionValidationDependencyException>(
                    registerServiceDescriptorFunction);

            //then
            actualServiceCollectionValidationDependencyException.Should().BeEquivalentTo(
                expectedServiceCollectionValidationDependencyException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.AddServiceDescriptor(It.IsAny<ServiceDescriptor>()),
                Times.Once);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }
    }
}
