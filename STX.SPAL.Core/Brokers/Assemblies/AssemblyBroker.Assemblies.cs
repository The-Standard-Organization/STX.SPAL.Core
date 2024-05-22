// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;

namespace STX.SPAL.Core.Brokers.Assemblies
{
    internal partial class AssemblyBroker
    {
        public Assembly GetAssembly(string assemblyPath) =>
            Assembly.LoadFrom(assemblyPath);
    }
}
