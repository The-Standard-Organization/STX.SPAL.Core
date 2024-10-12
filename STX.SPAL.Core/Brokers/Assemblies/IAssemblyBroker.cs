// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;
using System.Threading.Tasks;

namespace STX.SPAL.Core.Brokers.Assemblies
{
    internal partial interface IAssemblyBroker
    {
        ValueTask<string[]> GetApplicationPathsAssembliesAsync();
        ValueTask<Assembly> GetAssemblyAsync(string fullPath);
    }
}
