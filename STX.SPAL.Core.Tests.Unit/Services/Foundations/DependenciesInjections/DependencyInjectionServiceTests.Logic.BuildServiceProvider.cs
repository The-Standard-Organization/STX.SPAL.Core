// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.DependenciesInjections
{
    public partial class DependencyInjectionServiceTests
    {
        [Fact]
        private async Task ShouldBuildServiceProviderAsync()
        {
            // given
            dynamic randomProperties = CreateRandomProperties();
            dynamic inputProperties = randomProperties;

            ServiceDescriptor randomServiceDescriptor = randomProperties.ServiceDescriptor;
            ServiceDescriptor inputServiceDescriptor = randomServiceDescriptor;
            ServiceDescriptor expectedServiceDescriptor = inputServiceDescriptor;

            DependencyInjection inputDependencyInjection = inputProperties.DependencyInjection;
            inputDependencyInjection.ServiceCollection.Add(inputServiceDescriptor);

            DependencyInjection expectedDependencyInjection =
                new DependencyInjection
                {
                    ServiceCollection = inputDependencyInjection.ServiceCollection,
                    ServiceProvider = inputDependencyInjection.ServiceCollection.BuildServiceProvider()
                };

            DependencyInjection returnedDependencyInjection = expectedDependencyInjection;

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.BuildServiceProviderAsync(
                        It.Is<IServiceCollection>(actualServiceCollection =>
                            SameServiceCollectionAs(
                                actualServiceCollection,
                                expectedDependencyInjection.ServiceCollection)
                            .Compile()
                            .Invoke(inputDependencyInjection.ServiceCollection))))
                .ReturnsAsync(returnedDependencyInjection.ServiceProvider);

            // when
            DependencyInjection actualDependencyInjection =
               await this.dependencyInjectionService.BuildServiceProviderAsync(
                   inputProperties.DependencyInjection);

            // then
            actualDependencyInjection.Should().BeEquivalentTo(expectedDependencyInjection);

            this.dependencyInjectionBroker.Verify(
                broker =>
                    broker.BuildServiceProviderAsync(
                        It.Is<IServiceCollection>(actualServiceCollection =>
                            SameServiceCollectionAs(
                                actualServiceCollection,
                                expectedDependencyInjection.ServiceCollection)
                            .Compile()
                            .Invoke(actualDependencyInjection.ServiceCollection))),
                    Times.Once);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }
    }
}
