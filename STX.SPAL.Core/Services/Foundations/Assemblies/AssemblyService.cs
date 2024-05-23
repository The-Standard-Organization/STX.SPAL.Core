// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;
using STX.SPAL.Core.Brokers.Assemblies;

namespace STX.SPAL.Core.Services.Foundations.Assemblies
{
    internal partial class AssemblyService : IAssemblyService
    {
        private readonly IAssemblyBroker assemblyBroker;

        public AssemblyService(IAssemblyBroker assemblyBroker)
        {
            this.assemblyBroker = assemblyBroker;
        }

        public string[] GetApplicationPathsAssemblies() =>
            this.assemblyBroker.GetApplicationPathsAssemblies();

        public Assembly GetAssembly(string assemblyPath) =>
            TryCatch(() =>
            {
                ValidateAssemblyPath(assemblyPath);

                return this.assemblyBroker.GetAssembly(assemblyPath);
            });
    }
}
