// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;

namespace STX.SPAL.Core.Brokers.DependenciesInjection
{
    internal partial class DependencyInjectionBroker
    {
        public IServiceProvider BuildServiceProvider() =>
            serviceCollection.BuildServiceProvider();

        public T ResolveImplementation<T>(ServiceProvider serviceProvider) =>
            serviceProvider.GetRequiredService<T>();

        public T ResolveImplementation<T>(ServiceProvider serviceProvider, string spalId) =>
            serviceProvider.GetRequiredKeyedService<T>(spalId);
    }
}
