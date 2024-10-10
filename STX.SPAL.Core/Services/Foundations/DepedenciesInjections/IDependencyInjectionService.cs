// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;

namespace STX.SPAL.Core.Services.Foundations.DependenciesInjections
{
    internal partial interface IDependencyInjectionService
    {
        ValueTask<DependencyInjection> RegisterServiceDescriptorAsync(
            DependencyInjection dependencyInjection,
            Type spalInterfaceType,
            Type implementationType,
            ServiceLifetime serviceLifetime);

        ValueTask<DependencyInjection> RegisterServiceDescriptorAsync(
            DependencyInjection dependencyInjection,
            Type spalInterfaceType,
            string spalId,
            Type implementationType,
            ServiceLifetime serviceLifetime);

        ValueTask<DependencyInjection> BuildServiceProviderAsync(
            DependencyInjection dependencyInjection);

        ValueTask<T> GetServiceAsync<T>(
            DependencyInjection dependencyInjection);

        ValueTask<T> GetServiceAsync<T>(
            DependencyInjection dependencyInjection,
            string spalId);
    }
}
