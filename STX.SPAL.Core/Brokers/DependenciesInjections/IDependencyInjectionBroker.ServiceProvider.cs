// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;

namespace STX.SPAL.Core.Brokers.DependenciesInjections
{
    internal partial interface IDependencyInjectionBroker
    {
        IServiceProvider BuildServiceProvider(IServiceCollection serviceCollection);
        T ResolveImplementation<T>(IServiceProvider serviceProvider);
        T ResolveImplementation<T>(IServiceProvider serviceProvider, string spalId);
    }
}
