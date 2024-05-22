// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;
using System.Reflection;

namespace STX.SPAL.Core.Brokers.Assemblies
{
    internal partial class AssemblyBroker : IAssemblyBroker
    {
        private readonly string applicationPath;
        private readonly string[] applicationPathsAssemblies;

        public AssemblyBroker()
        {
            applicationPath =
                Path.GetDirectoryName(
                    Assembly.GetEntryAssembly().Location);

            applicationPathsAssemblies =
                Directory.GetFiles(applicationPath, "*.dll");
        }

        public string[] GetApplicationPathsAssemblies() =>
            applicationPathsAssemblies;

        public Assembly GetAssembly(string assemblyPath) =>
            Assembly.LoadFrom(assemblyPath);
    }
}
