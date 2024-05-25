// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace STX.SPAL.Core.Models.Services.Foundations.Assemblies.Exceptions
{
    internal class FailedAssemblyServiceException : Xeption
    {
        public FailedAssemblyServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
