// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace STX.SPAL.Core.Models.Services.Foundations.Assemblies.Exceptions
{
    internal class AssemblyLoadException : Xeption
    {
        public AssemblyLoadException(string message, Exception innerException)
            : base(message: message, innerException)
        { }
    }
}
