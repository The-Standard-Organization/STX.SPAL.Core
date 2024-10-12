// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections.Exceptions;

namespace STX.SPAL.Core.Services.Foundations.DependenciesInjections
{
    internal partial class DependencyInjectionService
    {
        private static dynamic IsInvalidObject<T>(T @object) => new
        {
            Condition = @object is null,
            Message = "object is required"
        };

        private static dynamic IsInvalidType(Type type) => new
        {
            Condition = type is null,
            Message = "Value is required"
        };

        private static dynamic IsInvalid(string @string) => new
        {
            Condition = string.IsNullOrWhiteSpace(@string),
            Message = "Value is required"
        };

        private static void ValidateDependencyInjection(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidDependencyInjectionParameterException =
                new InvalidDependencyInjectionParameterException (
                    message: "Invalid dependency injection parameter error occurred, fix errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidDependencyInjectionParameterException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidDependencyInjectionParameterException.ThrowIfContainsErrors();
        }

        private static void ValidateServiceDescriptor(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidServiceDescriptorParameterException =
                new InvalidServiceDescriptorParameterException(
                    message: "Invalid service descriptor parameter error occurred, fix errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidServiceDescriptorParameterException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidServiceDescriptorParameterException.ThrowIfContainsErrors();
        }

        private static void ValidateServiceCollection(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidServiceCollectionParameterException =
                new InvalidServiceCollectionParameterException(
                    message: "Invalid service collection parameter error occurred, fix errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidServiceCollectionParameterException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidServiceCollectionParameterException.ThrowIfContainsErrors();
        }

        private static void ValidateServiceProvider(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidServiceProviderParameterException =
                new InvalidServiceProviderParameterException(
                    message: "Invalid service provider parameter error occurred, fix errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidServiceProviderParameterException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidServiceProviderParameterException.ThrowIfContainsErrors();
        }

        private static void ValidateDependencyInjection(DependencyInjection dependencyInjection)
        {
            ValidateDependencyInjection(
                (Rule:
                    IsInvalidObject(dependencyInjection),
                Parameter:
                    nameof(DependencyInjection)));
        }

        private static void ValidateServiceDescriptorTypes(Type spalInterfaceType, Type implementationType)
        {
            ValidateServiceDescriptor(
                (Rule:
                    IsInvalidType(spalInterfaceType),
                Parameter:
                    nameof(spalInterfaceType)),

                (Rule:
                    IsInvalidType(implementationType),
                Parameter:
                    nameof(implementationType)));
        }

        private static void ValidateServiceDescriptorTypesWithSpalId(
            Type spalInterfaceType,
            string spalId,
            Type implementationType)
        {
            ValidateServiceDescriptor(
                (Rule:
                    IsInvalidType(spalInterfaceType),
                Parameter:
                    nameof(spalInterfaceType)),

                (Rule:
                    IsInvalid(spalId),
                Parameter:
                    nameof(spalId)),

                (Rule:
                    IsInvalidType(implementationType),
                Parameter:
                    nameof(implementationType)));
        }

        private static void ValidateServiceCollection(DependencyInjection dependencyInjection)
        {
            ValidateDependencyInjection(
                (Rule:
                    IsInvalidObject(dependencyInjection),
                Parameter:
                    nameof(DependencyInjection)));

            ValidateServiceCollection(
                (Rule:
                    IsInvalidObject(dependencyInjection.ServiceCollection),
                Parameter:
                    nameof(DependencyInjection.ServiceCollection)));
        }

        private static void ValidateServiceProvider(
            DependencyInjection dependencyInjection)
        {
            ValidateDependencyInjection(
                (Rule:
                    IsInvalidObject(dependencyInjection),
                Parameter:
                    nameof(DependencyInjection)));

            ValidateServiceProvider(
                (Rule:
                    IsInvalidObject(dependencyInjection.ServiceProvider),
                Parameter:
                    nameof(DependencyInjection.ServiceProvider)));
        }

        private static void ValidateServiceProviderWithSpalId(
            DependencyInjection dependencyInjection,
            string spalId)
        {
            ValidateDependencyInjection(
                (Rule:
                    IsInvalidObject(dependencyInjection),
                Parameter:
                    nameof(DependencyInjection)));

            ValidateServiceProvider(
                (Rule:
                    IsInvalidObject(dependencyInjection.ServiceProvider),
                Parameter:
                    nameof(DependencyInjection.ServiceProvider)),

                (Rule:
                    IsInvalid(spalId),
                Parameter:
                    nameof(spalId)));
        }
    }
}
