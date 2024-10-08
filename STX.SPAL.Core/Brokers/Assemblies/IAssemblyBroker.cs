﻿// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Reflection;

namespace STX.SPAL.Core.Brokers.Assemblies
{
    internal partial interface IAssemblyBroker
    {
        string[] GetApplicationPathsAssemblies();
        Assembly GetAssembly(string fullPath);
    }
}
