// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;
using System.Reflection;

namespace STX.SPAL.Core.Brokers.Assemblies
{
    internal partial class AssemblyBroker
    {
        public string[] GetApplicationAssemblies()
        {
            string applicationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            return Directory.GetFiles(applicationPath, "*.dll");
        }
    }
}
