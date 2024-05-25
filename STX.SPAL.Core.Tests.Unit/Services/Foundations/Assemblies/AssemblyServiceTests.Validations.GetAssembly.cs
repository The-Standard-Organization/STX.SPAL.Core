// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Reflection;
using FluentAssertions;
using Moq;
using STX.SPAL.Core.Models.Services.Foundations.Assemblies.Exceptions;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.Assemblies
{
    public partial class AssemblyServiceTests
    {
        [Theory]
        [InlineData(null, "Value is required")]
        [InlineData("", "Value is required")]
        [InlineData(" ", "Value is required")]
        [InlineData("file", "Value is not a valid assembly path")]
        public void ShouldThrowValidationExceptionIfInvalidAssemblyPath(
            string assemblyPath,
            string exceptionMessage)
        {
            // given
            Assembly randomAssembly = CreateRandomAssembly();
            Assembly expectedAssembly = randomAssembly;
            Assembly returnedAssembly = randomAssembly;
            string randomPathAssembly = CreateRandomPathAssembly();
            string inputAssemblyPath = assemblyPath;

            var invalidAssemblyPathException =
                new InvalidAssemblyPathException(
                    message: "Invalid assembly path error occurred, fix errors and try again.");

            invalidAssemblyPathException.AddData(
                key: nameof(assemblyPath),
                values: exceptionMessage
            );

            var expectedAssemblyValidationException =
                new AssemblyValidationException(
                    message: "Assembly validation error occurred, fix errors and try again.",
                    innerException: invalidAssemblyPathException);

            this.assemblyBroker
                .Setup(broker =>
                    broker.GetAssembly(
                        It.Is<string>(actualAssemblyPath =>
                            actualAssemblyPath == inputAssemblyPath)));

            // when
            Func<Assembly> getAssemblyFunction = () =>
                this.assemblyService.GetAssembly(inputAssemblyPath);

            AssemblyValidationException actualAssemblyValidationException =
                Assert.Throws<AssemblyValidationException>(
                    getAssemblyFunction);

            //then
            actualAssemblyValidationException.Should().BeEquivalentTo(
                expectedAssemblyValidationException);

            this.assemblyBroker
                .Verify(broker =>
                    broker.GetAssembly(It.IsAny<string>()),
                Times.Never);

            this.assemblyBroker.VerifyNoOtherCalls();
        }
    }
}
