// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;

namespace STX.SPAL.Core.Services.Foundations.Assemblies
{
    internal interface IAssemblyService
    {
        string[] GetApplicationPathsAssemblies();
        Assembly GetAssembly(string fullPath);
    }
}
