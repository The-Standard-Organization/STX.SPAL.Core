// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;

namespace STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections
{
    internal class DependencyInjection
    {
        public IServiceCollection ServiceCollection { get; init; }
        public IServiceProvider ServiceProvider { get; init; }
    }
}
