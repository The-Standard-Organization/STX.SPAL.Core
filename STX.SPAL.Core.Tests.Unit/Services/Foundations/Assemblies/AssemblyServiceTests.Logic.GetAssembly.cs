// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;
using FluentAssertions;
using Moq;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.Assemblies
{
    public partial class AssemblyServiceTests
    {
        [Fact]
        private void ShouldGetAssembly()
        {
            // given
            Assembly randomAssembly = CreateRandomAssembly();
            Assembly expectedAssembly = randomAssembly;
            Assembly returnedAssembly = randomAssembly;
            string randomPathAssembly = CreateRandomPathAssembly();
            string inputPathAssembly = randomPathAssembly;

            this.assemblyBroker
                .Setup(broker =>
                    broker.GetAssembly(
                        It.Is<string>(actualPathAssembly =>
                            actualPathAssembly == inputPathAssembly)))
                .Returns(returnedAssembly);

            // when
            Assembly actualAssembly =
               this.assemblyService.GetAssembly(inputPathAssembly);

            // then
            actualAssembly.Should().BeSameAs(expectedAssembly);

            this.assemblyBroker.Verify(
                broker =>
                    broker.GetAssembly(
                        It.Is<string>(actualPathAssembly =>
                            actualPathAssembly == inputPathAssembly)),
                    Times.Once);

            this.assemblyBroker.VerifyNoOtherCalls();
        }
    }
}
