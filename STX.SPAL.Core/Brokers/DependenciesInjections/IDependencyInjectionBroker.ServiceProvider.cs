// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace STX.SPAL.Core.Brokers.DependenciesInjections
{
    internal partial interface IDependencyInjectionBroker
    {
        ValueTask<IServiceProvider> BuildServiceProviderAsync(IServiceCollection serviceCollection);
        ValueTask<T> GetServiceAsync<T>(IServiceProvider serviceProvider);
        ValueTask<T> GetServiceAsync<T>(IServiceProvider serviceProvider, string spalId);
    }
}
