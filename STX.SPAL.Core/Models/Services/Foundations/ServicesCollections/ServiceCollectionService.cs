// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Core.Brokers.DependenciesInjection;

namespace STX.SPAL.Core.Models.Services.Foundations.ServicesCollections
{
    internal partial class ServiceCollectionService : IServiceCollectionService
    {
        private readonly IDependencyInjectionBroker dependencyInjectionBroker;

        public ServiceCollectionService(IDependencyInjectionBroker dependencyInjectionBroker)
        {
            this.dependencyInjectionBroker = dependencyInjectionBroker;
        }

        public IServiceCollection RegisterServiceDescriptor(
            Type spalInterfaceType,
            Type implementationType,
            ServiceLifetime serviceLifetime)
        {
            ServiceDescriptor serviceDescriptor =
                new ServiceDescriptor(spalInterfaceType, implementationType, serviceLifetime);

            return dependencyInjectionBroker.RegisterServiceDescriptor(serviceDescriptor);
        }

        public IServiceCollection RegisterServiceDescriptor(
            Type spalInterfaceType,
            string spalId,
            Type implementationType,
            ServiceLifetime serviceLifetime)
        {
            throw new NotImplementedException();
        }
    }
}
