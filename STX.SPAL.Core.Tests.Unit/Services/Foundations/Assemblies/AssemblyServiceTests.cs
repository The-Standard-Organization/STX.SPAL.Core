// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;
using System.Linq;
using Moq;
using STX.SPAL.Core.Brokers.Assemblies;
using STX.SPAL.Core.Services.Foundations.Assemblies;
using Tynamix.ObjectFiller;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.Assemblies
{
    public partial class AssemblyServiceTests
    {
        private readonly Mock<IAssemblyBroker> assemblyBroker;
        private readonly IAssemblyService assemblyService;

        public AssemblyServiceTests()
        {
            this.assemblyBroker = new Mock<IAssemblyBroker>();

            this.assemblyService =
                new AssemblyService(assemblyBroker.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string[] CreateRandomPathArray()
        {
            return Enumerable.Range(0, GetRandomNumber())
                .Select(i =>
                {
                    string randomPathName = GetRandomString();
                    string randomFileName = GetRandomString();

                    return $"{Path.Combine(randomPathName, randomFileName)}.dll";
                })
                .ToArray();
        }
    }
}
