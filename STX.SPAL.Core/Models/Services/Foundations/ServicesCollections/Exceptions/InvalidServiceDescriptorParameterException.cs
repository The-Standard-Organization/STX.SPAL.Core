// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace STX.SPAL.Core.Models.Services.Foundations.ServicesCollections.Exceptions
{
    internal class InvalidServiceDescriptorParameterException : Xeption
    {
        public InvalidServiceDescriptorParameterException(string message) : base(message)
        { }
    }
}
