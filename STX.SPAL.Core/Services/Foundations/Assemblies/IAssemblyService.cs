// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;
using System.Threading.Tasks;

namespace STX.SPAL.Core.Services.Foundations.Assemblies
{
    internal interface IAssemblyService
    {
        ValueTask<string[]> GetApplicationPathsAssembliesAsync();
        ValueTask<Assembly> GetAssemblyAsync(string fullPath);
    }
}
