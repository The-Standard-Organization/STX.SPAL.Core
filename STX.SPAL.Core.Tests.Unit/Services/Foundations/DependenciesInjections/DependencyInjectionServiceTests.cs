// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using STX.SPAL.Abstractions;
using STX.SPAL.Core.Brokers.DependenciesInjections;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections;
using STX.SPAL.Core.Models.Services.Foundations.DependenciesInjections.Exceptions;
using STX.SPAL.Core.Services.Foundations.DependenciesInjections;
using Tynamix.ObjectFiller;
using Xeptions;

namespace STX.SPAL.Core.Tests.Unit.Services.Foundations.DependenciesInjections
{
    public partial class DependencyInjectionServiceTests
    {
        private readonly Mock<IDependencyInjectionBroker> dependencyInjectionBroker;
        private readonly ICompareLogic compareLogic;
        private readonly IDependencyInjectionService dependencyInjectionService;

        public DependencyInjectionServiceTests()
        {
            this.dependencyInjectionBroker = new Mock<IDependencyInjectionBroker>();
            this.compareLogic = new CompareLogic();

            this.dependencyInjectionService =
                new DependencyInjectionService(dependencyInjectionBroker.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static AssemblyBuilder CreateRandomAssembly()
        {
            string randomAssemblyName = GetRandomString();

            var assemblyName =
                new AssemblyName(randomAssemblyName);

            AssemblyBuilder assemblyBuilder =
                AssemblyBuilder.DefineDynamicAssembly(
                    assemblyName,
                    AssemblyBuilderAccess.Run);

            return assemblyBuilder;
        }

        private static Type CreateRandomSpalInterfaceType()
        {
            AssemblyBuilder spalAssembly = CreateRandomAssembly();
            string assemblyName = spalAssembly.GetName().Name;
            ModuleBuilder moduleBuilder = spalAssembly.DefineDynamicModule(assemblyName);

            TypeBuilder typeBuilder = moduleBuilder.DefineType(
                name: GetRandomString(),
                attr: TypeAttributes.Public,
                parent: null,
                interfaces: new Type[]
                {
                    typeof(ISPALBase)
                });

            return typeBuilder;
        }

        private static Type CreateRandomImplementationType()
        {
            AssemblyBuilder spalAssembly = CreateRandomAssembly();
            string assemblyName = spalAssembly.GetName().Name;
            ModuleBuilder moduleBuilder = spalAssembly.DefineDynamicModule(assemblyName);

            TypeBuilder typeBuilder = moduleBuilder.DefineType(
                name: GetRandomString(),
                attr: TypeAttributes.Public,
                parent: null,
                interfaces: new Type[]
                {
                    typeof(ISPALBase)
                });

            return typeBuilder;
        }

        private static ServiceLifetime CreateRandomServiceLifeTime()
        {
            int randomNumber = new IntRange(min: 0, max: 2).GetValue();

            return randomNumber switch
            {
                0 => ServiceLifetime.Singleton,
                1 => ServiceLifetime.Scoped,
                _ => ServiceLifetime.Transient
            };
        }

        private static dynamic CreateRandomProperties()
        {
            Type randomSpalInterfaceType = CreateRandomSpalInterfaceType();
            string randomSpalId = GetRandomString();
            Type randomImplementationType = CreateRandomImplementationType();
            ServiceLifetime randomServiceLifeTime = CreateRandomServiceLifeTime();

            return new
            {
                SpalInterfaceType = randomSpalInterfaceType,
                SpalId = randomSpalId,
                ImplementationType = randomImplementationType,
                ServiceLifeTime = randomServiceLifeTime,

                ServiceDescriptor =
                    new ServiceDescriptor(
                        randomSpalInterfaceType,
                        randomImplementationType,
                        randomServiceLifeTime),

                ServiceDescriptorWithSpalId =
                    new ServiceDescriptor(
                        randomSpalInterfaceType,
                        randomSpalId,
                        randomImplementationType,
                        randomServiceLifeTime),

                DependencyInjection = new DependencyInjection
                {
                    ServiceCollection = new ServiceCollection()
                }
            };
        }

        private Expression<Func<ServiceDescriptor, bool>> SameServiceDescriptorAs(
            ServiceDescriptor actualServiceDescriptor,
            ServiceDescriptor expectedServiceDescriptor)
        {
            return actualServiceDescriptor =>
                this.compareLogic.Compare(
                    expectedServiceDescriptor,
                    actualServiceDescriptor)
                        .AreEqual;
        }

        private Expression<Func<IServiceCollection, bool>> SameServiceCollectionAs(
            IServiceCollection actualServiceCollection,
            IServiceCollection expectedServiceCollection)
        {
            return actualServiceCollection =>
                this.compareLogic.Compare(
                    expectedServiceCollection,
                    actualServiceCollection)
                        .AreEqual;
        }

        private Expression<Func<DependencyInjection, bool>> SameDependencyInjectionAs(
            DependencyInjection actualDependencyInjection,
            DependencyInjection expectedDependencyInjection)
        {
            return actualDependencyInjection =>
                this.compareLogic.Compare(
                    expectedDependencyInjection,
                    actualDependencyInjection)
                        .AreEqual;
        }

        private static Xeption CreateInvalidServiceDescriptorParameterException(
            IDictionary<string, string> parameters)
        {
            var invalidServiceDescriptorParameterException =
                new InvalidServiceDescriptorParameterException(
                    message: "Invalid service descriptor parameter error occurred, fix errors and try again.");

            parameters
                .Select(parameter =>
                {
                    invalidServiceDescriptorParameterException.UpsertDataList(
                        key: parameter.Key,
                        value: parameter.Value);

                    return invalidServiceDescriptorParameterException;
                })
                .ToArray();

            return invalidServiceDescriptorParameterException;
        }

        public static TheoryData<Type, Type, Xeption> RegisterServiceDescriptorValidationExceptions()
        {
            return new TheoryData<Type, Type, Xeption>
            {
                {
                    CreateRandomSpalInterfaceType(),
                    null,
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "implementationType", "Value is required" }
                        })
                },

                {
                    null,
                    CreateRandomImplementationType(),
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "spalInterfaceType", "Value is required" }
                        })
                },

                {
                    null,
                    null,
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "spalInterfaceType", "Value is required" },
                            { "implementationType", "Value is required" }
                        })
                },
            };
        }

        public static TheoryData<Type, string, Type, Xeption> RegisterServiceDescriptorWithSpalIdValidationExceptions()
        {
            return new TheoryData<Type, string, Type, Xeption>
            {
                {
                    CreateRandomSpalInterfaceType(),
                    GetRandomString(),
                    null,
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "implementationType", "Value is required" }
                        })
                },

                {
                    null,
                    GetRandomString(),
                    CreateRandomImplementationType(),
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "spalInterfaceType", "Value is required" }
                        })
                },

                {
                    null,
                    GetRandomString(),
                    null,
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "spalInterfaceType", "Value is required" },
                            { "implementationType", "Value is required" }
                        })
                },

                {
                    CreateRandomSpalInterfaceType(),
                    null,
                    null,
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "implementationType", "Value is required" },
                            { "spalId", "Value is required" }
                        })
                },

                {
                    null,
                    null,
                    CreateRandomImplementationType(),
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "spalInterfaceType", "Value is required" },
                            { "spalId", "Value is required" }
                        })
                },

                {
                    null,
                    null,
                    null,
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "spalInterfaceType", "Value is required" },
                            { "implementationType", "Value is required" },
                            { "spalId", "Value is required" }
                        })
                },

                {
                    CreateRandomSpalInterfaceType(),
                    "",
                    null,
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "implementationType", "Value is required" },
                            { "spalId", "Value is required" }
                        })
                },

                {
                    null,
                    "",
                    CreateRandomImplementationType(),
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "spalInterfaceType", "Value is required" },
                            { "spalId", "Value is required" }
                        })
                },

                {
                    null,
                    "",
                    null,
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "spalInterfaceType", "Value is required" },
                            { "implementationType", "Value is required" },
                            { "spalId", "Value is required" }
                        })
                },

                {
                    CreateRandomSpalInterfaceType(),
                    " ",
                    null,
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "implementationType", "Value is required" },
                            { "spalId", "Value is required" }
                        })
                },

                {
                    null,
                    " ",
                    CreateRandomImplementationType(),
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "spalInterfaceType", "Value is required" },
                            { "spalId", "Value is required" }
                        })
                },

                {
                    null,
                    " ",
                    null,
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "spalInterfaceType", "Value is required" },
                            { "implementationType", "Value is required" },
                            { "spalId", "Value is required" }
                        })
                },
            };
        }

        public static TheoryData<Exception> RegisterServiceDescriptorValidationDependencyExceptions()
        {
            return new TheoryData<Exception>
            {
                new ArgumentException()
            };
        }

        public static TheoryData<Exception> RegisterServiceDescriptorServiceExceptions()
        {
            return new TheoryData<Exception>
            {
                new Exception()
            };
        }
    }
}
