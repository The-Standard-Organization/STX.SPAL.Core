// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Core.Brokers.DependenciesInjections;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;

namespace STX.SPAL.Core.Services.Foundations.DependenciesInjections
{
    internal partial class DependencyInjectionService : IDependencyInjectionService
    {
        private readonly IDependencyInjectionBroker dependencyInjectionBroker;

        public DependencyInjectionService(IDependencyInjectionBroker dependencyInjectionBroker) =>
            this.dependencyInjectionBroker = dependencyInjectionBroker;

        public DependencyInjection RegisterServiceDescriptor(
            DependencyInjection dependencyInjection,
            Type spalInterfaceType,
            Type implementationType,
            ServiceLifetime serviceLifetime) =>
        TryCatch(() =>
        {
            ValidateDependencyInjection(dependencyInjection);
            ValidateServiceDescriptorTypes(spalInterfaceType, implementationType);

            ServiceDescriptor serviceDescriptor =
                new ServiceDescriptor(spalInterfaceType, implementationType, serviceLifetime);

            dependencyInjectionBroker.AddServiceDescriptor(
                dependencyInjection.ServiceCollection,
                serviceDescriptor);

            return dependencyInjection;
        });

        public DependencyInjection RegisterServiceDescriptor(
            DependencyInjection dependencyInjection,
            Type spalInterfaceType,
            string spalId,
            Type implementationType,
            ServiceLifetime serviceLifetime) =>
        TryCatch(() =>
        {
            ValidateServiceDescriptorTypesWithSpalId(spalInterfaceType, spalId, implementationType);

            ServiceDescriptor serviceDescriptor =
               new ServiceDescriptor(spalInterfaceType, spalId, implementationType, serviceLifetime);

            dependencyInjectionBroker.AddServiceDescriptor(
                dependencyInjection.ServiceCollection,
                serviceDescriptor);

            return dependencyInjection;
        });

        public DependencyInjection BuildServiceProvider(DependencyInjection dependencyInjection) =>
            TryCatch(() =>
            {
                ValidateServiceCollection(dependencyInjection);

                IServiceProvider serviceProvider =
                    dependencyInjectionBroker.BuildServiceProvider(
                        dependencyInjection.ServiceCollection);

                return new DependencyInjection
                {
                    ServiceCollection = dependencyInjection.ServiceCollection,
                    ServiceProvider = serviceProvider
                };
            });
    }
}
