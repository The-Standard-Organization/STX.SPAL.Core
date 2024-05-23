// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;
using STX.SPAL.Core.Models.Services.Foundations.Assemblies.Exceptions;
using Xeptions;

namespace STX.SPAL.Core.Services.Foundations.Assemblies
{
    internal partial class AssemblyService
    {
        private delegate Assembly ReturningAssemblyFunction();

        private static Assembly TryCatch(
            ReturningAssemblyFunction returningAssemblyFunction)
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
        }

        private static AssemblyValidationException CreateAssemblyValidationException(
            Xeption exception)
        {
            return new AssemblyValidationException(
                message: "Assembly validation error occurred, fix errors and try again.",
                innerException: exception);
        }
    }
}
