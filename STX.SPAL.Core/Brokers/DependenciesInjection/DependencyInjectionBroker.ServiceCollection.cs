﻿// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace STX.SPAL.Core.Brokers.DependenciesInjection
{
    internal partial class DependencyInjectionBroker
    {
        public void RegisterServiceDescriptor(ServiceDescriptor serviceDescriptor) =>
            serviceColllection.Add(serviceDescriptor);
    }
}