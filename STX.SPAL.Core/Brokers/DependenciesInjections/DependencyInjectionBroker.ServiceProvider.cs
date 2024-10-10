// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace STX.SPAL.Core.Brokers.DependenciesInjections
{
    internal partial class DependencyInjectionBroker
    {
        public async ValueTask<IServiceProvider> BuildServiceProviderAsync(IServiceCollection serviceCollection) =>
            serviceCollection.BuildServiceProvider();

        public async ValueTask<T> GetServiceAsync<T>(IServiceProvider serviceProvider) =>
            serviceProvider.GetRequiredService<T>();

        public async ValueTask<T> GetServiceAsync<T>(IServiceProvider serviceProvider, string spalId) =>
            serviceProvider.GetRequiredKeyedService<T>(spalId);
    }
}
