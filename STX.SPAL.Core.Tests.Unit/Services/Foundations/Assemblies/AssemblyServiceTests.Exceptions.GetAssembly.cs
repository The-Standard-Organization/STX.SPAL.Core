// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using STX.SPAL.Core.Models.Services.Foundations.Assemblies.Exceptions;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.Assemblies
{
    public partial class AssemblyServiceTests
    {
        [Theory]
        [MemberData(nameof(AssemblyLoadDependencyExceptions))]
        private async Task ShouldThrowDependencyExceptionOnLoadAssemblyIfExternalExceptionOccursAsync(
            Exception externalException)
        {
            // given
            string someAssemblyPath = CreateRandomPathAssembly();

            var assemblyLoadException =
                new AssemblyLoadException(
                    message: "Assembly load error occurred, contact support.",
                    innerException: externalException);

            var expectedAssemblyDependencyException =
                new AssemblyDependencyException(
                    message: "Assembly dependency error occurred, contact support.",
                    innerException: assemblyLoadException);

            this.assemblyBroker
                .Setup(broker =>
                    broker.GetAssemblyAsync(
                        It.Is<string>(actualAssemblyPath =>
                            actualAssemblyPath == someAssemblyPath)))
                .ThrowsAsync(externalException);

            // when
            Func<Task<Assembly>> getAssemblyFunction =
                () =>
                    this.assemblyService.GetAssemblyAsync(someAssemblyPath)
                        .AsTask();

            AssemblyDependencyException actualAssemblyDependencyException =
                await Assert.ThrowsAsync<AssemblyDependencyException>(
                    getAssemblyFunction);

            // then
            actualAssemblyDependencyException.Should().BeEquivalentTo(
                expectedAssemblyDependencyException);

            this.assemblyBroker
                .Verify(broker =>
                    broker.GetAssemblyAsync(It.IsAny<string>()),
                Times.Once);

            this.assemblyBroker.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AssemblyLoadValidationDependencyExceptions))]
        private async Task ShouldThrowValidationDependencyExceptionOnLoadAssemblyIfExternalExceptionOccursAsync(
            Exception externalException)
        {
            // given
            string someAssemblyPath = CreateRandomPathAssembly();

            var assemblyLoadException =
                new AssemblyLoadException(
                    message: "Assembly load error occurred, contact support.",
                    innerException: externalException);

            var expectedAssemblyValidationDependencyException =
                new AssemblyValidationDependencyException(
                    message: "Assembly validation dependency error occurred, contact support.",
                    innerException: assemblyLoadException);

            this.assemblyBroker
                .Setup(broker =>
                    broker.GetAssemblyAsync(
                        It.Is<string>(actualAssemblyPath =>
                            actualAssemblyPath == someAssemblyPath)))
                .Throws(externalException);

            // when
            Func<Task<Assembly>> getAssemblyFunction = () =>
                this.assemblyService.GetAssemblyAsync(someAssemblyPath)
                    .AsTask();

            AssemblyValidationDependencyException actualAssemblyValidationDependencyException =
                await Assert.ThrowsAsync<AssemblyValidationDependencyException>(
                    getAssemblyFunction);

            // then
            actualAssemblyValidationDependencyException.Should().BeEquivalentTo(
                expectedAssemblyValidationDependencyException);

            this.assemblyBroker
                .Verify(broker =>
                    broker.GetAssemblyAsync(It.IsAny<string>()),
                Times.Once);

            this.assemblyBroker.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AssemblyLoadServiceExceptions))]
        private async Task ShouldThrowServiceExceptionOnLoadAssemblyIfExceptionOccursAsync(
            Exception externalException)
        {
            // given
            string someAssemblyPath = CreateRandomPathAssembly();

            var assemblyLoadException =
                new FailedAssemblyServiceException(
                    message: "Failed service error occurred, contact support.",
                    innerException: externalException);

            var expectedAssemblyServiceException =
                new AssemblyServiceException(
                    message: "Assembly service error occurred, contact support.",
                    innerException: assemblyLoadException);

            this.assemblyBroker
                .Setup(broker =>
                    broker.GetAssemblyAsync(
                        It.Is<string>(actualAssemblyPath =>
                            actualAssemblyPath == someAssemblyPath)))
                .Throws(externalException);

            // when
            Func<Task<Assembly>> getAssemblyFunction =
                () =>
                    this.assemblyService.GetAssemblyAsync(someAssemblyPath)
                        .AsTask();

            AssemblyServiceException actualAssemblyServiceException =
                await Assert.ThrowsAsync<AssemblyServiceException>(
                    getAssemblyFunction);

            // then
            actualAssemblyServiceException.Should().BeEquivalentTo(
                expectedAssemblyServiceException);

            this.assemblyBroker
                .Verify(broker =>
                    broker.GetAssemblyAsync(It.IsAny<string>()),
                Times.Once);

            this.assemblyBroker.VerifyNoOtherCalls();
        }
    }
}
