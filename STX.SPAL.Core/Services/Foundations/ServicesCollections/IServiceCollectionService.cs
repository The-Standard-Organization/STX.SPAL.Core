// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;

namespace STX.SPAL.Core.Services.Foundations.ServicesCollections
{
    internal partial interface IServiceCollectionService
    {
        IServiceCollection RegisterServiceDescriptor(
            Type spalInterfaceType,
            Type implementationType,
            ServiceLifetime serviceLifetime);

        IServiceCollection RegisterServiceDescriptor(
            Type spalInterfaceType,
            string spalId,
            Type implementationType,
            ServiceLifetime serviceLifetime);
    }
}
