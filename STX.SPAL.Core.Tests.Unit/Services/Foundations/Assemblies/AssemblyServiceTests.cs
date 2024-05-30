// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
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

        private static string CreateRandomPathAssembly()
        {
            string randomPathName = GetRandomString();
            string randomFileName = GetRandomString();

            return $"{Path.Combine(randomPathName, randomFileName)}.dll";
        }

        private static string[] CreateRandomPathArray()
        {
            return Enumerable.Range(0, GetRandomNumber())
                .Select(i => CreateRandomPathAssembly())
                .ToArray();
        }

        private static Assembly CreateRandomAssembly()
        {
            string randomAssemblyName = GetRandomString();
            var assemblyName =
                new AssemblyName("randomAssemblyName");

            AssemblyBuilder assemblyBuilder =
                AssemblyBuilder.DefineDynamicAssembly(
                    assemblyName,
                    AssemblyBuilderAccess.Run);

            return assemblyBuilder;
        }

        public static TheoryData AssemblyLoadDependencyExceptions()
        {
            return new TheoryData<Exception>
            {
                new SecurityException(),
                new FileLoadException(),
                new FileNotFoundException(),
                new BadImageFormatException(),
                new InvalidOperationException(),
                new NotSupportedException(),
                new IOException(),
                new UnauthorizedAccessException()
            };
        }

        public static TheoryData AssemblyLoadValidationDependencyExceptions()
        {
            return new TheoryData<Exception>
            {
                new ArgumentException()
            };
        }

        public static TheoryData AssemblyLoadServiceExceptions()
        {
            return new TheoryData<Exception>
            {
                new Exception()
            };
        }
    }
}
