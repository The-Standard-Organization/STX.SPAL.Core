// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.SPAL.Core.Models.Services.Foundations.Assemblies.Exceptions;

namespace STX.SPAL.Core.Services.Foundations.Assemblies
{
    internal partial class AssemblyService
    {
        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Value is required"
        };

        private static dynamic IsInvalidAssemblyPath(string assemblyPath) =>
            new
            {
                Condition = !assemblyPath.EndsWith(".dll"),
                Message = "Value is not a valid assembly path"
            };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidAssemblyPathException =
                new InvalidAssemblyPathException(
                    message: "Invalid assembly path error occurred, fix errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAssemblyPathException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAssemblyPathException.ThrowIfContainsErrors();
        }

        private static void ValidateAssemblyPath(string assemblyPath)
        {
            Validate(
                (Rule: IsInvalid(assemblyPath), Parameter: nameof(assemblyPath)));

            Validate(
                (Rule: IsInvalidAssemblyPath(assemblyPath), Parameter: nameof(assemblyPath)));
        }
    }
}
