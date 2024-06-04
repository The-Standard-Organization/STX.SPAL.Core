// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.ServicesCollections
{
    public partial class ServiceCollectionServiceTests
    {
        [Fact]
        private void ShouldRegisterServiceDescriptor()
        {
            // given
            dynamic randomProperties = CreateRandomProperties();
            dynamic inputProperties = randomProperties;

            ServiceDescriptor randomServiceDescriptor = randomProperties.ServiceDescriptor;
            ServiceDescriptor inputServiceDescriptor = randomServiceDescriptor;
            ServiceDescriptor expectedServiceDescriptor = inputServiceDescriptor;

            IServiceCollection randomServiceCollection = randomProperties.ServiceCollection;
            IServiceCollection expectedServiceCollection = randomServiceCollection;
            IServiceCollection returnedServiceCollection = randomServiceCollection;

            expectedServiceCollection.Add(inputServiceDescriptor);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.AddServiceDescriptor(
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            SameServiceDescriptorAs(
                                actualServiceDescriptor,
                                expectedServiceDescriptor)
                            .Compile()
                            .Invoke(actualServiceDescriptor))))
                .Returns(returnedServiceCollection);

            // when
            IServiceCollection actualServiceCollection =
               this.serviceCollectionService.RegisterServiceDescriptor(
                   inputProperties.SpalInterfaceType,
                   inputProperties.ImplementationType,
                   inputProperties.ServiceLifeTime);

            //then
            actualServiceCollection.Should().BeEquivalentTo(expectedServiceCollection);

            this.dependencyInjectionBroker.Verify(
                broker =>
                    broker.AddServiceDescriptor(
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            SameServiceDescriptorAs(
                                actualServiceDescriptor,
                                expectedServiceDescriptor)
                            .Compile()
                            .Invoke(actualServiceDescriptor))),
                    Times.Once);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }

        [Fact]
        private void ShouldRegisterServiceDescriptorWithSpalId()
        {
            // given
            dynamic randomProperties = CreateRandomProperties();
            dynamic inputProperties = randomProperties;

            ServiceDescriptor randomServiceDescriptor = randomProperties.ServiceDescriptorWithSpalId;
            ServiceDescriptor inputServiceDescriptor = randomServiceDescriptor;
            ServiceDescriptor expectedServiceDescriptor = inputServiceDescriptor;

            IServiceCollection randomServiceCollection = randomProperties.ServiceCollection;
            IServiceCollection expectedServiceCollection = randomServiceCollection;
            IServiceCollection returnedServiceCollection = randomServiceCollection;

            expectedServiceCollection.Add(inputServiceDescriptor);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.AddServiceDescriptor(
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            SameServiceDescriptorAs(
                                actualServiceDescriptor,
                                expectedServiceDescriptor)
                            .Compile()
                            .Invoke(actualServiceDescriptor))))
                .Returns(returnedServiceCollection);

            // when
            IServiceCollection actualServiceCollection =
               this.serviceCollectionService.RegisterServiceDescriptor(
                   inputProperties.SpalInterfaceType,
                   inputProperties.SpalId,
                   inputProperties.ImplementationType,
                   inputProperties.ServiceLifeTime);

            //then
            actualServiceCollection.Should().BeEquivalentTo(expectedServiceCollection);

            this.dependencyInjectionBroker.Verify(
                broker =>
                    broker.AddServiceDescriptor(
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            SameServiceDescriptorAs(
                                actualServiceDescriptor,
                                expectedServiceDescriptor)
                            .Compile()
                            .Invoke(actualServiceDescriptor))),
                    Times.Once);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }
    }
}
