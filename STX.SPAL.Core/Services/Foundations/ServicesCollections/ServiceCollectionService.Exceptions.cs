// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using STX.SPAL.Core.Models.Services.Foundations.ServicesCollections.Exceptions;
using Xeptions;

namespace STX.SPAL.Core.Services.Foundations.ServicesCollections
{
    internal partial class ServiceCollectionService
    {
        private delegate IServiceCollection ReturningServiceCollectionFunction();

        private static IServiceCollection TryCatch(ReturningServiceCollectionFunction returningServiceCollectionFunction)
        {
            try
            {
                return returningServiceCollectionFunction();
            }

            catch (InvalidServiceDescriptorParameterException invalidServiceDescriptorParameterException)
            {
                throw CreateServiceCollectionValidationException(invalidServiceDescriptorParameterException);
            }
        }

        private static ServiceCollectionValidationException CreateServiceCollectionValidationException(Xeption exception)
        {
            return new ServiceCollectionValidationException(
                message: "Service Collection validation error occurred, fix errors and try again.",
                innerException: exception);
        }
    }
}
