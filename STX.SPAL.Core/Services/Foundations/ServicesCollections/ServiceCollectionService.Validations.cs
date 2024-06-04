// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using STX.SPAL.Core.Models.Services.Foundations.ServicesCollections.Exceptions;

namespace STX.SPAL.Core.Services.Foundations.ServicesCollections
{
    internal partial class ServiceCollectionService
    {
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

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
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

        private static void ValidateServiceDescriptorTypes(Type spalInterfaceType, Type implementationType)
        {
            Validate(
                (Rule: IsInvalidType(spalInterfaceType), Parameter: nameof(spalInterfaceType)),
                (Rule: IsInvalidType(implementationType), Parameter: nameof(implementationType)));
        }

        private static void ValidateServiceDescriptorTypesWithSpalId(
            Type spalInterfaceType,
            string spalId,
            Type implementationType)
        {
            Validate(
                (Rule: IsInvalidType(spalInterfaceType), Parameter: nameof(spalInterfaceType)),
                (Rule: IsInvalid(spalId), Parameter: nameof(spalId)),
                (Rule: IsInvalidType(implementationType), Parameter: nameof(implementationType)));
        }
    }
}
