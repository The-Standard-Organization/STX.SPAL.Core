// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.Assemblies
{
    public partial class AssemblyServiceTests
    {
        [Fact]
        private async Task ShouldGetAssemblyAsync()
        {
            // given
            Assembly randomAssembly = CreateRandomAssembly();
            Assembly expectedAssembly = randomAssembly;
            Assembly returnedAssembly = randomAssembly;
            string randomPathAssembly = CreateRandomPathAssembly();
            string inputPathAssembly = randomPathAssembly;

            this.assemblyBroker
                .Setup(broker =>
                    broker.GetAssemblyAsync(
                        It.Is<string>(actualPathAssembly =>
                            actualPathAssembly == inputPathAssembly)))
                .ReturnsAsync(returnedAssembly);

            // when
            Assembly actualAssembly =
               await this.assemblyService.GetAssemblyAsync(inputPathAssembly);

            // then
            actualAssembly.Should().BeSameAs(expectedAssembly);

            this.assemblyBroker.Verify(
                broker =>
                    broker.GetAssemblyAsync(
                        It.Is<string>(actualPathAssembly =>
                            actualPathAssembly == inputPathAssembly)),
                    Times.Once);

            this.assemblyBroker.VerifyNoOtherCalls();
        }
    }
}
