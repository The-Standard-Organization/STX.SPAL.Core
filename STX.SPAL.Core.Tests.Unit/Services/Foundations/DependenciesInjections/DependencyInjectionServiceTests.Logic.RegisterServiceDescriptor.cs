// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
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
        private async Task ShouldRegisterServiceDescriptorAsync()
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
                    broker.AddServiceDescriptorAsync(
                        It.IsAny<IServiceCollection>(),
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            SameServiceDescriptorAs(
                                actualServiceDescriptor,
                                expectedServiceDescriptor)
                            .Compile()
                            .Invoke(actualServiceDescriptor))))
                .ReturnsAsync(returnedDependencyInjection.ServiceCollection);

            // when
            DependencyInjection actualDependencyInjection =
               await this.dependencyInjectionService.RegisterServiceDescriptorAsync(
                   inputProperties.DependencyInjection,
                   inputProperties.SpalInterfaceType,
                   inputProperties.ImplementationType,
                   inputProperties.ServiceLifeTime);

            // then
            actualDependencyInjection.Should().BeEquivalentTo(expectedDependencyInjection);

            this.dependencyInjectionBroker.Verify(
                broker =>
                    broker.AddServiceDescriptorAsync(
                        It.IsAny<IServiceCollection>(),
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
        private async Task ShouldRegisterServiceDescriptorWithSpalIdAsync()
        {
            // given
            dynamic randomProperties = CreateRandomProperties();
            dynamic inputProperties = randomProperties;

            ServiceDescriptor randomServiceDescriptor = randomProperties.ServiceDescriptorWithSpalId;
            ServiceDescriptor inputServiceDescriptor = randomServiceDescriptor;
            ServiceDescriptor expectedServiceDescriptor = inputServiceDescriptor;

            DependencyInjection inputDependencyInjection = inputProperties.DependencyInjection;
            DependencyInjection expectedDependencyInjection = inputDependencyInjection.DeepClone();
            DependencyInjection returnedDependencyInjection = inputDependencyInjection.DeepClone();
            IServiceCollection inputServiceCollection = inputDependencyInjection.ServiceCollection;

            expectedDependencyInjection.ServiceCollection.Add(inputServiceDescriptor);

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.AddServiceDescriptorAsync(
                        It.IsAny<IServiceCollection>(),
                        It.Is<ServiceDescriptor>(actualServiceDescriptor =>
                            SameServiceDescriptorAs(
                                actualServiceDescriptor,
                                expectedServiceDescriptor)
                            .Compile()
                            .Invoke(actualServiceDescriptor))))
                .ReturnsAsync(returnedDependencyInjection.ServiceCollection);

            // when
            DependencyInjection actualDependencyInjection =
               await this.dependencyInjectionService.RegisterServiceDescriptorAsync(
                   inputProperties.DependencyInjection,
                   inputProperties.SpalInterfaceType,
                   inputProperties.SpalId,
                   inputProperties.ImplementationType,
                   inputProperties.ServiceLifeTime);

            // then
            actualDependencyInjection.Should().BeEquivalentTo(expectedDependencyInjection);

            this.dependencyInjectionBroker.Verify(
                broker =>
                    broker.AddServiceDescriptorAsync(
                        It.IsAny<IServiceCollection>(),
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
