// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using FluentAssertions;
using Force.DeepCloner;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.DependenciesInjections
{
    public partial class DependencyInjectionServiceTests
    {
        [Fact]
        private void ShouldBuildServiceProvider()
        {
            // given
            dynamic randomProperties = CreateRandomProperties();
            dynamic inputProperties = randomProperties;

            ServiceDescriptor randomServiceDescriptor = randomProperties.ServiceDescriptor;
            ServiceDescriptor inputServiceDescriptor = randomServiceDescriptor;
            ServiceDescriptor expectedServiceDescriptor = inputServiceDescriptor;

            DependencyInjection inputDependencyInjection = inputProperties.DependencyInjection;
            DependencyInjection expectedDependencyInjection = inputDependencyInjection.DeepClone();
            DependencyInjection returnedDependencyInjection = inputDependencyInjection.DeepClone();
            IServiceCollection inputServiceCollection = inputDependencyInjection.ServiceCollection;

            expectedDependencyInjection.ServiceCollection.Add(inputServiceDescriptor);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.BuildServiceProvider(
                        It.Is<IServiceCollection>(actualServiceCollection =>
                            SameServiceCollectionAs(
                                actualServiceCollection,
                                expectedDependencyInjection.ServiceCollection)
                            .Compile()
                            .Invoke(inputServiceCollection))))
                .Returns(inputDependencyInjection.ServiceProvider);

            // when
            DependencyInjection actualDependencyInjection =
               this.dependencyInjectionService.BuildServiceProvider(
                   inputProperties.DependencyInjection);

            //then
            actualDependencyInjection.Should().BeEquivalentTo(expectedDependencyInjection);

            this.dependencyInjectionBroker.Verify(
                broker =>
                    broker.BuildServiceProvider(
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
