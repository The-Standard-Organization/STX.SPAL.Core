// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections.Exceptions
{
    internal class FailedDependencyInjectionServiceException : Xeption
    {
        public FailedDependencyInjectionServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
