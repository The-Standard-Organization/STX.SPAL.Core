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
        private void ShouldGetApplicationPathAssemblies()
        {
            // given
            string[] randomApplicationPathsAssemblies =
                CreateRandomPathArray();

            string[] expectedApplicationPathsAssemblies =
                randomApplicationPathsAssemblies;

            string[] returnedApplicationPathsAssemblies =
               randomApplicationPathsAssemblies;

            this.assemblyBroker
                .Setup(broker => broker.GetApplicationPathsAssemblies())
                .Returns(returnedApplicationPathsAssemblies);

            // when
            string[] actualApplicationPathsAssemblies =
               this.assemblyService.GetApplicationPathsAssemblies();

            //then
            actualApplicationPathsAssemblies.Should()
                .BeEquivalentTo(expectedApplicationPathsAssemblies);

            this.assemblyBroker.Verify(
                broker =>
                    broker.GetApplicationPathsAssemblies(),
                Times.Once);

            this.assemblyBroker.VerifyNoOtherCalls();
        }
    }
}
