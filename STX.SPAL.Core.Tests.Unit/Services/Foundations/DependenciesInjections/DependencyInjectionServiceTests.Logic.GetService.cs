// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using STX.SPAL.Abstractions;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.DependenciesInjections
{
    public partial class DependencyInjectionServiceTests
    {
        [Fact]
        private void ShouldGetService()
        {
            // given
            dynamic randomProperties = CreateRandomProperties();
            dynamic inputProperties = randomProperties;

            ServiceDescriptor randomServiceDescriptor = randomProperties.ServiceDescriptor;
            ServiceDescriptor inputServiceDescriptor = randomServiceDescriptor;
            ServiceDescriptor expectedServiceDescriptor = inputServiceDescriptor;


            IServiceCollection inputServiceCollection = inputProperties.DependencyInjection.ServiceCollection;
            inputServiceCollection.Add(inputServiceDescriptor);

            DependencyInjection inputDependencyInjection =
                new DependencyInjection
                {
                    ServiceCollection = inputServiceCollection,
                    ServiceProvider = inputServiceCollection.BuildServiceProvider()
                };

            Type implementationType = randomProperties.ImplementationType;

            ISPALBase returnedService =
                Activator.CreateInstance(implementationType) as ISPALBase;

            ISPALBase expectedService = returnedService;

            DependencyInjection expectedDependencyInjection = inputDependencyInjection;
            DependencyInjection returnedDependencyInjection = expectedDependencyInjection;

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.GetService<ISPALBase>(
                        It.Is<IServiceProvider>(actualServiceProvider =>
                            SameServiceProviderAs(
                                actualServiceProvider,
                                expectedDependencyInjection.ServiceProvider)
                            .Compile()
                            .Invoke(inputDependencyInjection.ServiceProvider))))
                .Returns(returnedService);

            // when
            ISPALBase actualService =
               this.dependencyInjectionService.GetService<ISPALBase>(
                   inputProperties.DependencyInjection);

            //then
            actualService.Should().BeEquivalentTo(expectedService);

            this.dependencyInjectionBroker.Verify(
                broker =>
                    broker.GetService<ISPALBase>(
                        It.Is<IServiceProvider>(actualServiceProvider =>
                            SameServiceProviderAs(
                                actualServiceProvider,
                                expectedDependencyInjection.ServiceProvider)
                            .Compile()
                            .Invoke(inputDependencyInjection.ServiceProvider))),
                    Times.Once);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }

        [Fact]
        private void ShouldGetServiceWithSpalId()
        {
            // given
            dynamic randomProperties = CreateRandomProperties();
            dynamic inputProperties = randomProperties;

            ServiceDescriptor randomServiceDescriptor = randomProperties.ServiceDescriptorWithSpalId;
            ServiceDescriptor inputServiceDescriptor = randomServiceDescriptor;
            ServiceDescriptor expectedServiceDescriptor = inputServiceDescriptor;


            IServiceCollection inputServiceCollection = inputProperties.DependencyInjection.ServiceCollection;
            inputServiceCollection.Add(inputServiceDescriptor);

            DependencyInjection inputDependencyInjection =
                new DependencyInjection
                {
                    ServiceCollection = inputServiceCollection,
                    ServiceProvider = inputServiceCollection.BuildServiceProvider()
                };

            Type implementationType = randomProperties.ImplementationType;

            ISPALBase returnedService =
                Activator.CreateInstance(implementationType) as ISPALBase;

            ISPALBase expectedService = returnedService;

            DependencyInjection expectedDependencyInjection = inputDependencyInjection;
            DependencyInjection returnedDependencyInjection = expectedDependencyInjection;

            this.dependencyInjectionBroker
                .Setup(broker =>
                    broker.GetService<ISPALBase>(
                        It.Is<IServiceProvider>(actualServiceProvider =>
                            SameServiceProviderAs(
                                actualServiceProvider,
                                expectedDependencyInjection.ServiceProvider)
                            .Compile()
                            .Invoke(inputDependencyInjection.ServiceProvider))))
                .Returns(returnedService);

            // when
            ISPALBase actualService =
               this.dependencyInjectionService.GetService<ISPALBase>(
                   inputProperties.DependencyInjection,
                   inputProperties.SpalId);

            //then
            actualService.Should().BeEquivalentTo(expectedService);

            this.dependencyInjectionBroker.Verify(
                broker =>
                    broker.GetService<ISPALBase>(
                        It.Is<IServiceProvider>(actualServiceProvider =>
                            SameServiceProviderAs(
                                actualServiceProvider,
                                expectedDependencyInjection.ServiceProvider)
                            .Compile()
                            .Invoke(inputDependencyInjection.ServiceProvider))),
                    Times.Once);

            this.dependencyInjectionBroker.VerifyNoOtherCalls();
        }
    }
}
