// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;

namespace STX.SPAL.Core.Models.Services.Foundations.ServicesCollections
{
    internal partial interface IServiceCollectionService
    {
        IServiceCollection RegisterServiceDescriptor(
            Type spalInterfaceType,
            Type implementationType,
            ServiceLifetime serviceLifetime);
    }
}
