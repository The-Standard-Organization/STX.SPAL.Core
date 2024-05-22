// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;

namespace STX.SPAL.Core.Brokers.Assemblies
{
    internal partial class AssemblyBroker
    {
        public string[] GetApplicationAssemblies() =>
            Directory.GetFiles(applicationPath, "*.dll");
    }
}
