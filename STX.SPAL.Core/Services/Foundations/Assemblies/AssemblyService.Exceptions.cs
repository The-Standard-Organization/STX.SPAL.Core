// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using System.Security;
using STX.SPAL.Core.Models.Services.Foundations.Assemblies.Exceptions;
using Xeptions;

namespace STX.SPAL.Core.Services.Foundations.Assemblies
{
    internal partial class AssemblyService
    {
        private delegate Assembly ReturningAssemblyFunction();

        private static Assembly TryCatch(ReturningAssemblyFunction returningAssemblyFunction)
        {
            try
            {
                return returningAssemblyFunction();
            }

            catch (InvalidAssemblyPathException invalidAssemblyPathException)
            {
                throw CreateAssemblyValidationException(
                    invalidAssemblyPathException);
            }

            catch (SecurityException securityException)
            {
                var assemblyLoadException =
                    new AssemblyLoadException(
                        message: "Assembly load error occurred, contact support.",
                        innerException: securityException);

                throw CreateAssemblyDependencyException(
                    assemblyLoadException);
            }

            catch (FileLoadException fileLoadException)
            {
                var assemblyLoadException =
                    new AssemblyLoadException(
                        message: "Assembly load error occurred, contact support.",
                        innerException: fileLoadException);

                throw CreateAssemblyDependencyException(
                    assemblyLoadException);
            }

            catch (FileNotFoundException fileNotFoundException)
            {
                var assemblyLoadException =
                    new AssemblyLoadException(
                        message: "Assembly load error occurred, contact support.",
                        innerException: fileNotFoundException);

                throw CreateAssemblyDependencyException(
                    assemblyLoadException);
            }

            catch (BadImageFormatException badImageFormatException)
            {
                var assemblyLoadException =
                    new AssemblyLoadException(
                        message: "Assembly load error occurred, contact support.",
                        innerException: badImageFormatException);

                throw CreateAssemblyDependencyException(
                    assemblyLoadException);
            }

            catch (InvalidOperationException invalidOperationException)
            {
                var assemblyLoadException =
                    new AssemblyLoadException(
                        message: "Assembly load error occurred, contact support.",
                        innerException: invalidOperationException);

                throw CreateAssemblyDependencyException(
                    assemblyLoadException);
            }

            catch (NotSupportedException notSupportedException)
            {
                var assemblyLoadException =
                    new AssemblyLoadException(
                        message: "Assembly load error occurred, contact support.",
                        innerException: notSupportedException);

                throw CreateAssemblyDependencyException(
                    assemblyLoadException);
            }

            catch (IOException iOException)
            {
                var assemblyLoadException =
                    new AssemblyLoadException(
                        message: "Assembly load error occurred, contact support.",
                        innerException: iOException);

                throw CreateAssemblyDependencyException(assemblyLoadException);
            }

            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                var assemblyLoadException =
                    new AssemblyLoadException(
                        message: "Assembly load error occurred, contact support.",
                        innerException: unauthorizedAccessException);

                throw CreateAssemblyDependencyException(
                    assemblyLoadException);
            }

            catch (ArgumentException argumentException)
            {
                var assemblyLoadException =
                    new AssemblyLoadException(
                        message: "Assembly load error occurred, contact support.",
                        innerException: argumentException);

                throw CreateAssemblyValidationDependencyException(
                    assemblyLoadException);
            }

            catch (Exception exception)
            {
                var failedAssemblyServiceException =
                    new FailedAssemblyServiceException(
                        message: "Failed service error occurred, contact support.",
                        innerException: exception);

                throw CreateAssemblyServiceException(
                    failedAssemblyServiceException);
            }
        }

        private static AssemblyValidationException CreateAssemblyValidationException(Xeption exception)
        {
            return new AssemblyValidationException(
                message: "Assembly validation error occurred, fix errors and try again.",
                innerException: exception);
        }

        private static AssemblyDependencyException CreateAssemblyDependencyException(Xeption exception)
        {
            return new AssemblyDependencyException(
                message: "Assembly dependency error occurred, contact support.",
                innerException: exception);
        }

        private static AssemblyValidationDependencyException
            CreateAssemblyValidationDependencyException(Xeption exception)
        {
            return new AssemblyValidationDependencyException(
                message: "Assembly validation dependency error occurred, contact support.",
                innerException: exception);
        }

        private static AssemblyServiceException CreateAssemblyServiceException(Xeption exception)
        {
            return new AssemblyServiceException(
                message: "Assembly service error occurred, contact support.",
                innerException: exception);
        }
    }
}
