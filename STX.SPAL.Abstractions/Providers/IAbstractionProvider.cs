// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace STX.SPAL.Abstractions.Providers
{
    public interface IAbstractionProvider<T>
        where T : ISPALProvider
    {
        T GetProvider();

        void UseProvider(string spalId = null);

        void UseProvider(
            Type concreteProviderType = null,
            string spalId = null);

        void UseProvider<TConcreteProviderType>(
            string spalId = null)
            where TConcreteProviderType : T;

        void Invoke<TResult>(Action<T> spalFunction);

        void InvokeWithProvider<TConcreteProviderType, TResult>(
            Action<T> spalFunction)
            where TConcreteProviderType : T;

        TResult Invoke<TResult>(
            Func<T, TResult> spalFunction);

        ValueTask InvokeAsync<TResult>(
            Func<T, ValueTask> spalFunction);

        ValueTask<TResult> InvokeAsync<TResult>(
            Func<T, ValueTask<TResult>> spalFunction);

    }
}
