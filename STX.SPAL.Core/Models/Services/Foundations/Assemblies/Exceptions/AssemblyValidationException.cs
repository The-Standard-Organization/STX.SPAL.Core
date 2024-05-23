// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace STX.SPAL.Core.Models.Services.Foundations.Assemblies.Exceptions
{
    public class AssemblyValidationException : Xeption
    {
        public AssemblyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}