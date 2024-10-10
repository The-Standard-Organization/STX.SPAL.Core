// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.Assemblies
{
    public partial class AssemblyServiceTests
    {
        [Fact]
        private async Task ShouldGetApplicationPathAssemblies()
        {
            // given
            string[] randomApplicationPathsAssemblies =
                CreateRandomPathArray();

            string[] expectedApplicationPathsAssemblies =
                randomApplicationPathsAssemblies;

            string[] returnedApplicationPathsAssemblies =
               randomApplicationPathsAssemblies;

            this.assemblyBroker
                .Setup(broker => broker.GetApplicationPathsAssembliesAsync())
                .ReturnsAsync(returnedApplicationPathsAssemblies);

            // when
            string[] actualApplicationPathsAssemblies =
               await this.assemblyService.GetApplicationPathsAssembliesAsync();

            // then
            actualApplicationPathsAssemblies.Should()
                .BeEquivalentTo(expectedApplicationPathsAssemblies);

            this.assemblyBroker.Verify(
                broker =>
                    broker.GetApplicationPathsAssembliesAsync(),
                Times.Once);

            this.assemblyBroker.VerifyNoOtherCalls();
        }
    }
}
