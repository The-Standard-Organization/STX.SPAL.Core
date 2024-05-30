// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace STX.SPAL.Core.Models.Services.Foundations.Assemblies.Exceptions
{
    internal class AssemblyServiceException : Xeption
    {
        public AssemblyServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
