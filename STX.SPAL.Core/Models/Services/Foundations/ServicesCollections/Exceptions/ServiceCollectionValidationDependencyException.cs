// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace STX.SPAL.Core.Models.Services.Foundations.ServicesCollections.Exceptions
{
    internal class ServiceCollectionValidationDependencyException : Xeption
    {
        public ServiceCollectionValidationDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
