// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.IO;
using System;
using System.Reflection;
using System.Security;
using STX.SPAL.Core.Models.Services.Foundations.Assemblies.Exceptions;
using Xeptions;
using System.Runtime.InteropServices;

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
                throw CreateAssemblyDependencyException(
                    securityException);
            }

            catch (FileLoadException fileLoadException)
            {
                throw CreateAssemblyDependencyException(
                    fileLoadException);
            }

            catch (FileNotFoundException fileNotFoundException)
            {
                throw CreateAssemblyDependencyException(
                    fileNotFoundException);
            }

            catch (BadImageFormatException badImageFormatException)
            {
                throw CreateAssemblyDependencyException(
                    badImageFormatException);
            }

            catch (InvalidOperationException invalidOperationException)
            {
                throw CreateAssemblyDependencyException(
                    invalidOperationException);
            }

            catch (NotSupportedException notSupportedException)
            {
                throw CreateAssemblyDependencyException(
                    notSupportedException);
            }

            catch (IOException iOException)
            {
                throw CreateAssemblyDependencyException(iOException);
            }

            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                throw CreateAssemblyDependencyException(
                    unauthorizedAccessException);
            }
        }

        private static AssemblyValidationException CreateAssemblyValidationException(Xeption exception)
        {
            return new AssemblyValidationException(
                message: "Assembly validation error occurred, fix errors and try again.",
                innerException: exception);
        }

        private static AssemblyDependencyException CreateAssemblyDependencyException(Exception exception)
        {
            var assemblyLoadException =
               new AssemblyLoadException(
                   message: "Assembly load error occurred, contact support.",
                   innerException: exception);

            return new AssemblyDependencyException(
                    message: "Assembly dependency error occurred, contact support.",
                    innerException: assemblyLoadException);
        }
    }
}
