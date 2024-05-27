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
        [MemberData(nameof(AssemblyLoadDependencyExceptions))]
        private void ShouldThrowDependencyExceptionOnLoadAssemblyIfExternalExceptionOccurs(
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
                    broker.GetAssembly(
                        It.Is<string>(actualAssemblyPath =>
                            actualAssemblyPath == someAssemblyPath)))
                .Throws(externalException);

            // when
            Func<Assembly> getAssemblyFunction = () =>
                this.assemblyService.GetAssembly(someAssemblyPath);

            AssemblyDependencyException actualAssemblyDependencyException =
                Assert.Throws<AssemblyDependencyException>(
                    getAssemblyFunction);

            //then
            actualAssemblyDependencyException.Should().BeEquivalentTo(
                expectedAssemblyDependencyException);

            this.assemblyBroker
                .Verify(broker =>
                    broker.GetAssembly(It.IsAny<string>()),
                Times.Once);

            this.assemblyBroker.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AssemblyLoadValidationDependencyExceptions))]
        private void ShouldThrowValidationDependencyExceptionOnLoadAssemblyIfExternalExceptionOccurs(
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
                    broker.GetAssembly(
                        It.Is<string>(actualAssemblyPath =>
                            actualAssemblyPath == someAssemblyPath)))
                .Throws(externalException);

            // when
            Func<Assembly> getAssemblyFunction = () =>
                this.assemblyService.GetAssembly(someAssemblyPath);

            AssemblyValidationDependencyException actualAssemblyValidationDependencyException =
                Assert.Throws<AssemblyValidationDependencyException>(
                    getAssemblyFunction);

            //then
            actualAssemblyValidationDependencyException.Should().BeEquivalentTo(
                expectedAssemblyValidationDependencyException);

            this.assemblyBroker
                .Verify(broker =>
                    broker.GetAssembly(It.IsAny<string>()),
                Times.Once);

            this.assemblyBroker.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AssemblyLoadServiceExceptions))]
        private void ShouldThrowServiceExceptionOnLoadAssemblyIfExceptionOccurs(
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
                    broker.GetAssembly(
                        It.Is<string>(actualAssemblyPath =>
                            actualAssemblyPath == someAssemblyPath)))
                .Throws(externalException);

            // when
            Func<Assembly> getAssemblyFunction = () =>
                this.assemblyService.GetAssembly(someAssemblyPath);

            AssemblyServiceException actualAssemblyServiceException =
                Assert.Throws<AssemblyServiceException>(
                    getAssemblyFunction);

            //then
            actualAssemblyServiceException.Should().BeEquivalentTo(
                expectedAssemblyServiceException);

            this.assemblyBroker
                .Verify(broker =>
                    broker.GetAssembly(It.IsAny<string>()),
                Times.Once);

            this.assemblyBroker.VerifyNoOtherCalls();
        }
    }
}
