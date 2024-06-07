// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace STX.SPAL.Core.Models.Services.Foundations.ServicesCollections.Exceptions
{
    internal class ServiceCollectionServiceException : Xeption
    {
        public ServiceCollectionServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
