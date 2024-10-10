// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
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

        public ValueTask<DependencyInjection> RegisterServiceDescriptorAsync(
            DependencyInjection dependencyInjection,
            Type spalInterfaceType,
            Type implementationType,
            ServiceLifetime serviceLifetime) =>
        TryCatch(async () =>
        {
            ValidateDependencyInjection(dependencyInjection);
            ValidateServiceDescriptorTypes(spalInterfaceType, implementationType);

            ServiceDescriptor serviceDescriptor =
                new ServiceDescriptor(spalInterfaceType, implementationType, serviceLifetime);

            await dependencyInjectionBroker.AddServiceDescriptorAsync(
                dependencyInjection.ServiceCollection,
                serviceDescriptor);

            return dependencyInjection;
        });

        public ValueTask<DependencyInjection> RegisterServiceDescriptorAsync(
            DependencyInjection dependencyInjection,
            Type spalInterfaceType,
            string spalId,
            Type implementationType,
            ServiceLifetime serviceLifetime) =>
        TryCatch(async () =>
        {
            ValidateDependencyInjection(dependencyInjection);
            ValidateServiceDescriptorTypesWithSpalId(spalInterfaceType, spalId, implementationType);

            ServiceDescriptor serviceDescriptor =
               new ServiceDescriptor(spalInterfaceType, spalId, implementationType, serviceLifetime);

            await dependencyInjectionBroker.AddServiceDescriptorAsync(
                dependencyInjection.ServiceCollection,
                serviceDescriptor);

            return dependencyInjection;
        });

        public ValueTask<DependencyInjection> BuildServiceProviderAsync(DependencyInjection dependencyInjection) =>
            TryCatch(async () =>
            {
                ValidateServiceCollection(dependencyInjection);

                IServiceProvider serviceProvider =
                    await dependencyInjectionBroker.BuildServiceProviderAsync(
                        dependencyInjection.ServiceCollection);

                return new DependencyInjection
                {
                    ServiceCollection = dependencyInjection.ServiceCollection,
                    ServiceProvider = serviceProvider
                };
            });

        public ValueTask<T> GetServiceAsync<T>(DependencyInjection dependencyInjection) =>
            TryCatchGetService(async () =>
            {
                ValidateServiceProvider(dependencyInjection);

                return await dependencyInjectionBroker.GetServiceAsync<T>(
                    dependencyInjection.ServiceProvider);
            });

        public ValueTask<T> GetServiceAsync<T>(DependencyInjection dependencyInjection, string spalId) =>
            TryCatchGetService(async () =>
            {
                ValidateServiceProviderWithSpalId(dependencyInjection, spalId);

                return await dependencyInjectionBroker.GetServiceAsync<T>(
                    dependencyInjection.ServiceProvider,
                    spalId);
            });
    }
}
