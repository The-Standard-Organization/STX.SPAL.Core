// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections.Exceptions
{
    public class DependencyInjectionValidationException : Xeption
    {
        public DependencyInjectionValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}