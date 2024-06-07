// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace STX.SPAL.Core.Models.Services.Foundations.ServicesCollections.Exceptions
{
    internal class FailedServiceCollectionServiceException : Xeption
    {
        public FailedServiceCollectionServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
