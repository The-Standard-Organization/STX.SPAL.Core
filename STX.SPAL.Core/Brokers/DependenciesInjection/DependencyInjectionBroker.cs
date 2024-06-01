// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace STX.SPAL.Core.Brokers.DependenciesInjection
{
    internal partial class DependencyInjectionBroker : IDependencyInjectionBroker
    {
        private readonly ServiceCollection serviceColllection;

        public DependencyInjectionBroker()
        {
            serviceColllection = new ServiceCollection();
        }
    }
}
