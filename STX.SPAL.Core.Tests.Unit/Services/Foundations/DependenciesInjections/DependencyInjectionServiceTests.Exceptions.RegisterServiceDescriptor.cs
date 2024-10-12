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

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.DependenciesInjections
{
    public partial class DependencyInjectionServiceTests
    {
        [Theory]
        [MemberData(nameof(RegisterServiceDescriptorValidationDependencyExceptions))]
        private async Task ShouldThrowValidationDependencyExceptionOnRegisterServiceDescriptorIfExternalExceptionOccursAsync(
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
                    broker.AddServiceDescriptorAsync(
                        someDependencyInjection.ServiceCollection,
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            SameServiceDescriptorAs(
                                actualServiceDescriptor,
                                someServiceDescriptor)
                            .Compile()
                            .Invoke(actualServiceDescriptor))))
                .ThrowsAsync(externalException);

            // when
            Func<Task<DependencyInjection>> registerServiceDescriptorFunction =
                () =>
                    this.dependencyInjectionService.RegisterServiceDescriptorAsync(
                        randomProperties.DependencyInjection,
                        randomProperties.SpalInterfaceType,
                        randomProperties.ImplementationType,
                        randomProperties.ServiceLifeTime)
                    .AsTask();

            DependencyInjectionValidationDependencyException actualServiceCollectionValidationDependencyException =
                await Assert.ThrowsAsync<DependencyInjectionValidationDependencyException>(
                    registerServiceDescriptorFunction);

            // then
            actualServiceCollectionValidationDependencyException.Should().BeEquivalentTo(
                expectedServiceCollectionValidationDependencyException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.AddServiceDescriptorAsync(
                        someDependencyInjection.ServiceCollection,
                        It.IsAny<ServiceDescriptor>()),
                Times.Once);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(RegisterServiceDescriptorServiceExceptions))]
        private async Task ShouldThrowServiceExceptionOnRegisterServiceDescriptorIfExceptionOccursAsync(
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

            var expectedDependencyInjectionServiceException =
                new DependencyInjectionServiceException(
                    message: "Dependency Injection service error occurred, contact support.",
                    innerException: assemblyLoadException);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.AddServiceDescriptorAsync(
                        someDependencyInjection.ServiceCollection,
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            SameServiceDescriptorAs(
                                actualServiceDescriptor,
                                someServiceDescriptor)
                            .Compile()
                            .Invoke(actualServiceDescriptor))))
                .ThrowsAsync(externalException);

            // when
            Func<Task<DependencyInjection>> registerServiceDescriptorFunction =
                () =>
                    this.dependencyInjectionService.RegisterServiceDescriptorAsync(
                        someDependencyInjection,
                        randomProperties.SpalInterfaceType,
                        randomProperties.ImplementationType,
                        randomProperties.ServiceLifeTime)
                    .AsTask();

            DependencyInjectionServiceException actualDependencyInjectionServiceException =
                await Assert.ThrowsAsync<DependencyInjectionServiceException>(
                    registerServiceDescriptorFunction);

            // then
            actualDependencyInjectionServiceException.Should().BeEquivalentTo(
                expectedDependencyInjectionServiceException);

            this.dependencyInjectionBroker
                .Verify(broker =>
                    broker.AddServiceDescriptorAsync(
                        someDependencyInjection.ServiceCollection,
                        It.IsAny<ServiceDescriptor>()),
                Times.Once);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }
    }
}
