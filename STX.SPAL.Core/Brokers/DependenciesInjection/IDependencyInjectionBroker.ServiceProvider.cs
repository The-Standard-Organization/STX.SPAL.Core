﻿// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;

namespace STX.SPAL.Core.Brokers.DependenciesInjection
{
    internal partial interface IDependencyInjectionBroker
    {
        IServiceProvider BuildServiceProvider();
        T ResolveImplementation<T>(ServiceProvider serviceProvider);
        T ResolveImplementation<T>(ServiceProvider serviceProvider, string spalId);
    }
}
