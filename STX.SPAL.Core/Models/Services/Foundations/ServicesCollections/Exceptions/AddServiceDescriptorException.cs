// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace STX.SPAL.Core.Models.Services.Foundations.ServicesCollections.Exceptions
{
    internal class AddServiceDescriptorException : Xeption
    {
        public AddServiceDescriptorException(string message, Exception innerException)
            : base(message: message, innerException)
        { }
    }
}
