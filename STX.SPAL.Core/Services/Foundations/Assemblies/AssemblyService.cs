// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;
using System.Threading.Tasks;
using STX.SPAL.Core.Brokers.Assemblies;

namespace STX.SPAL.Core.Services.Foundations.Assemblies
{
    internal partial class AssemblyService : IAssemblyService
    {
        private readonly IAssemblyBroker assemblyBroker;

        public AssemblyService(IAssemblyBroker assemblyBroker) =>
            this.assemblyBroker = assemblyBroker;

        public async ValueTask<string[]> GetApplicationPathsAssembliesAsync() =>
            await this.assemblyBroker.GetApplicationPathsAssembliesAsync();

        public ValueTask<Assembly> GetAssemblyAsync(string assemblyPath) =>
            TryCatch(async () =>
            {
                ValidateAssemblyPath(assemblyPath);

                return await this.assemblyBroker.GetAssemblyAsync(assemblyPath);
            });
    }
}
