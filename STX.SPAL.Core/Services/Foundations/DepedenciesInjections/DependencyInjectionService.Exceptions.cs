// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections.Exceptions;
using Xeptions;

namespace STX.SPAL.Core.Services.Foundations.DependenciesInjections
{
    internal partial class DependencyInjectionService
    {
        private delegate DependencyInjection ReturningDependencyInjectionFunction();

        private static DependencyInjection TryCatch(
            ReturningDependencyInjectionFunction returningDependencyInjectionFunction)
        {
            try
            {
                return returningDependencyInjectionFunction();
            }

            catch (InvalidServiceDescriptorParameterException invalidServiceDescriptorParameterException)
            {
                throw CreateDependencyInjectionValidationException(
                    invalidServiceDescriptorParameterException);
            }

            catch (ArgumentException argumentException)
            {
                var addServiceDescriptorException =
                    new AddServiceDescriptorException(
                        message: "Add service descriptor error occurred, contact support.",
                        innerException: argumentException);

                throw CreateDependencyInjectionValidationDependencyException(
                    addServiceDescriptorException);
            }

            catch (Exception exception)
            {
                var failedServiceCollectionServiceException =
                    new FailedDependencyInjectionServiceException(
                        message: "Failed service error occurred, contact support.",
                        innerException: exception);

                throw CreateDependencyInjectionServiceException(
                    failedServiceCollectionServiceException);
            }
        }

        private static DependencyInjectionValidationException CreateDependencyInjectionValidationException(
            Xeption exception)
        {
            return new DependencyInjectionValidationException(
                message: "Service Collection validation error occurred, fix errors and try again.",
                innerException: exception);
        }

        private static DependencyInjectionValidationDependencyException
            CreateDependencyInjectionValidationDependencyException(Xeption exception)
        {
            return new DependencyInjectionValidationDependencyException(
                message: "Service collection validation dependency error occurred, contact support.",
                innerException: exception);
        }

        private static DependencyInjectionServiceException CreateDependencyInjectionServiceException(
            Xeption exception)
        {
            return new DependencyInjectionServiceException(
                message: "ServiceCollection service error occurred, contact support.",
                innerException: exception);
        }
    }
}
