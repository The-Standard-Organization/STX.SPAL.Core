// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections.Exceptions;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.DependenciesInjections
{
    public partial class DependencyInjectionServiceTests
    {
        [Theory]
        [MemberData(nameof(RegisterServiceDescriptorValidationDependencyExceptions))]
        private void ShouldThrowValidationDependencyExceptionOnRegisterServiceDescriptorIfExternalExceptionOccurs(
            Exception externalException)
        {
            // given
            dynamic randomProperties = CreateRandomProperties();
            DependencyInjection someDependencyInjection = randomProperties.DependencyInjection;
            ServiceDescriptor someServiceDescriptor = randomProperties.ServiceDescriptor;

            var addServiceDescriptorException =
                new AddServiceDescriptorException(
                    message: "Add service descriptor error occurred, contact support.",
                    innerException: externalException);

            var expectedServiceCollectionValidationDependencyException =
                new DependencyInjectionValidationDependencyException(
                    message: "Dependency Injection validation dependency error occurred, contact support.",
                    innerException: addServiceDescriptorException);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.AddServiceDescriptor(
                        someDependencyInjection.ServiceCollection,
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            SameServiceDescriptorAs(
                                actualServiceDescriptor,
                                someServiceDescriptor)
                            .Compile()
                            .Invoke(actualServiceDescriptor))))
                .Throws(externalException);

            // when
            Func<DependencyInjection> registerServiceDescriptorFunction = () =>
                this.dependencyInjectionService.RegisterServiceDescriptor(
                    randomProperties.DependencyInjection,
                    randomProperties.SpalInterfaceType,
                    randomProperties.ImplementationType,
                    randomProperties.ServiceLifeTime);

            DependencyInjectionValidationDependencyException actualServiceCollectionValidationDependencyException =
                Assert.Throws<DependencyInjectionValidationDependencyException>(
                    registerServiceDescriptorFunction);

            // then
            actualServiceCollectionValidationDependencyException.Should().BeEquivalentTo(
                expectedServiceCollectionValidationDependencyException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.AddServiceDescriptor(
                        someDependencyInjection.ServiceCollection,
                        It.IsAny<ServiceDescriptor>()),
                Times.Once);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(RegisterServiceDescriptorServiceExceptions))]
        private void ShouldThrowServiceExceptionOnRegisterServiceDescriptorIfExceptionOccurs(
            Exception externalException)
        {
            // given
            dynamic randomProperties = CreateRandomProperties();
            DependencyInjection someDependencyInjection = randomProperties.DependencyInjection;
            ServiceDescriptor someServiceDescriptor = randomProperties.ServiceDescriptor;

            var assemblyLoadException =
                new FailedDependencyInjectionServiceException(
                    message: "Failed service error occurred, contact support.",
                    innerException: externalException);

            var expectedServiceCollectionServiceException =
                new DependencyInjectionServiceException(
                    message: "Dependency Injection service error occurred, contact support.",
                    innerException: assemblyLoadException);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.AddServiceDescriptor(
                        someDependencyInjection.ServiceCollection,
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            SameServiceDescriptorAs(
                                actualServiceDescriptor,
                                someServiceDescriptor)
                            .Compile()
                            .Invoke(actualServiceDescriptor))))
                .Throws(externalException);

            // when
            Func<IServiceCollection> registerServiceDescriptorFunction = () =>
                this.dependencyInjectionService.RegisterServiceDescriptor(
                    someDependencyInjection,
                    randomProperties.SpalInterfaceType,
                    randomProperties.ImplementationType,
                    randomProperties.ServiceLifeTime);

            DependencyInjectionServiceException actualServiceCollectionServiceException =
                Assert.Throws<DependencyInjectionServiceException>(
                    registerServiceDescriptorFunction);

            // then
            actualServiceCollectionServiceException.Should().BeEquivalentTo(
                expectedServiceCollectionServiceException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.AddServiceDescriptor(
                        someDependencyInjection.ServiceCollection,
                        It.IsAny<ServiceDescriptor>()),
                Times.Once);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }
    }
}
