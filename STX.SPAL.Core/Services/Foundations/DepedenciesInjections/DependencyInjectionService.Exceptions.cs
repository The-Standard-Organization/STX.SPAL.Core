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
        private delegate T ReturningGetServiceDependencyInjectionFunction<T>();

        private static DependencyInjection TryCatch(
            ReturningDependencyInjectionFunction returningDependencyInjectionFunction)
        {
            try
            {
                return returningDependencyInjectionFunction();
            }

            catch (InvalidDependencyInjectionParameterException invalidDependencyInjectionParameterException)
            {
                throw CreateDependencyInjectionValidationException(
                    invalidDependencyInjectionParameterException);
            }

            catch (InvalidServiceDescriptorParameterException invalidServiceDescriptorParameterException)
            {
                throw CreateDependencyInjectionValidationException(
                    invalidServiceDescriptorParameterException);
            }

            catch (InvalidServiceCollectionParameterException invalidServiceCollectionParameterException)
            {
                throw CreateDependencyInjectionValidationException(
                    invalidServiceCollectionParameterException);
            }

            catch (InvalidServiceProviderParameterException invalidServiceProviderParameterException)
            {
                throw CreateDependencyInjectionValidationException(
                    invalidServiceProviderParameterException);
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

        private static T TryCatchGetService<T>(
            ReturningGetServiceDependencyInjectionFunction<T> returningGetServiceDependencyInjectionFunction)
        {
            try
            {
                return returningGetServiceDependencyInjectionFunction();
            }

            catch (InvalidDependencyInjectionParameterException invalidDependencyInjectionParameterException)
            {
                throw CreateDependencyInjectionValidationException(
                    invalidDependencyInjectionParameterException);
            }

            catch (InvalidServiceProviderParameterException invalidServiceProviderParameterException)
            {
                throw CreateDependencyInjectionValidationException(
                    invalidServiceProviderParameterException);
            }
        }

        private static DependencyInjectionValidationException CreateDependencyInjectionValidationException(
            Xeption exception)
        {
            return new DependencyInjectionValidationException(
                message: "Dependency Injection validation error occurred, fix errors and try again.",
                innerException: exception);
        }

        private static DependencyInjectionValidationDependencyException
            CreateDependencyInjectionValidationDependencyException(Xeption exception)
        {
            return new DependencyInjectionValidationDependencyException(
                message: "Dependency Injection validation dependency error occurred, contact support.",
                innerException: exception);
        }

        private static DependencyInjectionServiceException CreateDependencyInjectionServiceException(
            Xeption exception)
        {
            return new DependencyInjectionServiceException(
                message: "Dependency Injection service error occurred, contact support.",
                innerException: exception);
        }
    }
}
