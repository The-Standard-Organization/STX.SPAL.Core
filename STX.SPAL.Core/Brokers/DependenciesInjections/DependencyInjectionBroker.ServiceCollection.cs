// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace STX.SPAL.Core.Brokers.DependenciesInjections
{
    internal partial class DependencyInjectionBroker
    {
        public async ValueTask<IServiceCollection> AddServiceDescriptorAsync(
                IServiceCollection serviceCollection,
                ServiceDescriptor serviceDescriptor)
        {
            serviceCollection.Add(serviceDescriptor);
            return serviceCollection;
        }
    }
}
