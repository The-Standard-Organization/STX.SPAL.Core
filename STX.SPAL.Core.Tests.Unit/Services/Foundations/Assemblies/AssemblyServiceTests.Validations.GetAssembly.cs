// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Reflection;
using Moq;
using STX.SPAL.Core.Models.Services.Foundations.Assemblies.Exceptions;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.Assemblies
{
    public partial class AssemblyServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ShouldThrowExceptionIfInvalidAssemblyPath(string inputPathAssembly)
        {
            // given
            Assembly randomAssembly = CreateRandomAssembly();
            Assembly expectedAssembly = randomAssembly;
            Assembly returnedAssembly = randomAssembly;
            string randomPathAssembly = GetRandomPathAssembly();

            this.assemblyBroker
                .Setup(broker =>
                    broker.GetAssembly(
                        It.Is<string>(actualPathAssembly =>
                                actualPathAssembly == inputPathAssembly)));

            // when
            Func<Assembly> getAssemblyFunction = () =>
                this.assemblyService.GetAssembly(inputPathAssembly);

            Assert.Throws<InvalidAssemblyPathException>(
                getAssemblyFunction);

            //then
            this.assemblyBroker.VerifyNoOtherCalls();
        }
    }
}
