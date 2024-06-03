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

            IServiceCollection randomServiceCollection = randomProperties.ServiceCollection;
            IServiceCollection expectedServiceCollection = randomServiceCollection;
            IServiceCollection returnedServiceCollection = randomServiceCollection;

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.RegisterServiceDescriptor(
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            actualServiceDescriptor == inputServiceDescriptor)))
                .Returns(returnedServiceCollection);

            // when
            IServiceCollection actualServiceCollection =
               this.serviceCollectionService.RegisterServiceDescriptor(
                   inputProperties.SpalInterfaceType,
                   inputProperties.ImplementationType,
                   inputProperties.ServiceLifeTime);

            //then
            actualServiceCollection.Should().BeSameAs(expectedServiceCollection);

            this.dependencyInjectionBroker.Verify(
                broker =>
                    broker.RegisterServiceDescriptor(
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            actualServiceDescriptor == inputServiceDescriptor)),
                    Times.Once);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }
    }
}
