// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;

namespace STX.SPAL.Core.Services.Foundations.DependenciesInjections
{
    internal partial interface IDependencyInjectionService
    {
        DependencyInjection RegisterServiceDescriptor(
            DependencyInjection dependencyInjection,
            Type spalInterfaceType,
            Type implementationType,
            ServiceLifetime serviceLifetime);

        DependencyInjection RegisterServiceDescriptor(
            DependencyInjection dependencyInjection,
            Type spalInterfaceType,
            string spalId,
            Type implementationType,
            ServiceLifetime serviceLifetime);

        DependencyInjection BuildServiceProvider(
            DependencyInjection dependencyInjection);

        T GetService<T>(
            DependencyInjection dependencyInjection);

        T GetService<T>(
            DependencyInjection dependencyInjection,
            string spalId);
    }
}
