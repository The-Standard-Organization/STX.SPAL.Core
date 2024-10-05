// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;

namespace STX.SPAL.Core.Brokers.DependenciesInjections
{
    internal partial class DependencyInjectionBroker
    {
        public IServiceProvider BuildServiceProvider(IServiceCollection serviceCollection) =>
            serviceCollection.BuildServiceProvider();

        public T ResolveImplementation<T>(IServiceProvider serviceProvider) =>
            serviceProvider.GetRequiredService<T>();

        public T ResolveImplementation<T>(IServiceProvider serviceProvider, string spalId) =>
            serviceProvider.GetRequiredKeyedService<T>(spalId);
    }
}
