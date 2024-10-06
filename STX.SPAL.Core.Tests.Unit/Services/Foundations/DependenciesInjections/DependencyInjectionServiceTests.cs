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
            Type iSpalBaseType = typeof(ISPALBase);

            AssemblyBuilder spalAssembly = CreateRandomAssembly();
            string assemblyName = spalAssembly.GetName().Name;
            ModuleBuilder moduleBuilder = spalAssembly.DefineDynamicModule(assemblyName);

            TypeBuilder typeBuilder = moduleBuilder.DefineType(
                name: GetRandomString(),
                attr: TypeAttributes.Public
                    | TypeAttributes.Interface
                    | TypeAttributes.Abstract,
                parent: null,
                interfaces: new Type[]
                {
                    iSpalBaseType
                });

            return typeBuilder.CreateType();
        }

        private static Type CreateRandomImplementationType()
        {
            Type iSpalBaseType = typeof(ISPALBase);

            AssemblyBuilder spalAssembly = CreateRandomAssembly();
            string assemblyName = spalAssembly.GetName().Name;
            ModuleBuilder moduleBuilder = spalAssembly.DefineDynamicModule(assemblyName);

            TypeBuilder typeBuilder = moduleBuilder.DefineType(
                name: GetRandomString(),
                attr:
                    TypeAttributes.Public
                    | TypeAttributes.Class);

            typeBuilder.AddInterfaceImplementation(iSpalBaseType);

            MethodInfo methodInfoGetSPALId =
                iSpalBaseType
                    .GetMethod(nameof(ISPALBase.GetSPALId));

            MethodBuilder getSPALIDMethodBuilder =
                typeBuilder
                    .DefineMethod(
                        name: nameof(ISPALBase.GetSPALId),
                        attributes:
                            MethodAttributes.Public
                            | MethodAttributes.Final
                            | MethodAttributes.HideBySig
                            | MethodAttributes.NewSlot
                            | MethodAttributes.Virtual,

                        returnType: typeof(string),
                        parameterTypes: Type.EmptyTypes);

            ILGenerator ilGenerator =
                getSPALIDMethodBuilder.GetILGenerator();

            ilGenerator.Emit(OpCodes.Ldstr, "Hello SPAL ID");
            ilGenerator.Emit(OpCodes.Ret);

            typeBuilder.DefineMethodOverride(
                getSPALIDMethodBuilder,
                iSpalBaseType.GetMethod(nameof(ISPALBase.GetSPALId)));

            return typeBuilder.CreateType();
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

        private Expression<Func<IServiceProvider, bool>> SameServiceProviderAs(
            IServiceProvider actualServiceProvider,
            IServiceProvider expectedServiceProvider)
        {
            return actualServiceProvider =>
                this.compareLogic.Compare(
                    expectedServiceProvider,
                    actualServiceProvider)
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

        private static Xeption CreateInvalidServiceCollectionParameterException(
            IDictionary<string, string> parameters)
        {
            var invalidServiceCollectionParameterException =
                new InvalidServiceCollectionParameterException(
                    message: "Invalid service collection parameter error occurred, fix errors and try again.");

            parameters
                .Select(parameter =>
                {
                    invalidServiceCollectionParameterException.UpsertDataList(
                        key: parameter.Key,
                        value: parameter.Value);

                    return invalidServiceCollectionParameterException;
                })
                .ToArray();

            return invalidServiceCollectionParameterException;
        }

        private static Xeption CreateInvalidDependencyInjectionParameterException(
            IDictionary<string, string> parameters)
        {
            var invalidDependencyInjectionParameterException =
                new InvalidDependencyInjectionParameterException(
                    message: "Invalid dependency injection parameter error occurred, fix errors and try again.");

            parameters
                .Select(parameter =>
                {
                    invalidDependencyInjectionParameterException.UpsertDataList(
                        key: parameter.Key,
                        value: parameter.Value);

                    return invalidDependencyInjectionParameterException;
                })
                .ToArray();

            return invalidDependencyInjectionParameterException;
        }

        private static Xeption CreateInvalidServiceProviderParameterException(
            IDictionary<string, string> parameters)
        {
            var invalidServiceProviderParameterException =
                new InvalidServiceProviderParameterException(
                    message: "Invalid service provider parameter error occurred, fix errors and try again.");

            parameters
                .Select(parameter =>
                {
                    invalidServiceProviderParameterException.UpsertDataList(
                        key: parameter.Key,
                        value: parameter.Value);

                    return invalidServiceProviderParameterException;
                })
                .ToArray();

            return invalidServiceProviderParameterException;
        }

        public static TheoryData<DependencyInjection, Type, Type, Xeption> RegisterServiceDescriptorValidationExceptions()
        {
            dynamic randomProperties = CreateRandomProperties();
            DependencyInjection someDependencyInjection = randomProperties.DependencyInjection;

            return new TheoryData<DependencyInjection, Type, Type, Xeption>
            {
                {
                    null,
                    null,
                    null,
                    CreateInvalidDependencyInjectionParameterException(
                        new Dictionary<string, string>
                        {
                            { nameof(DependencyInjection), "object is required" }
                        })
                },

                {
                    someDependencyInjection,
                    CreateRandomSpalInterfaceType(),
                    null,
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "implementationType", "Value is required" }
                        })
                },

                {
                    someDependencyInjection,
                    null,
                    CreateRandomImplementationType(),
                    CreateInvalidServiceDescriptorParameterException(
                        new Dictionary<string, string>
                        {
                            { "spalInterfaceType", "Value is required" }
                        })
                },

                {
                    someDependencyInjection,
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

        public static TheoryData<DependencyInjection, Type, string, Type, Xeption> RegisterServiceDescriptorWithSpalIdValidationExceptions()
        {
            dynamic randomProperties = CreateRandomProperties();
            DependencyInjection someDependencyInjection = randomProperties.DependencyInjection;

            return new TheoryData<DependencyInjection, Type, string, Type, Xeption>
            {
                {
                    null,
                    null,
                    null,
                    null,
                    CreateInvalidDependencyInjectionParameterException(
                        new Dictionary<string, string>
                        {
                            { nameof(DependencyInjection), "object is required" }
                        })
                },

                {
                    someDependencyInjection,
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
                    someDependencyInjection,
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
                    someDependencyInjection,
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
                    someDependencyInjection,
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
                    someDependencyInjection,
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
                    someDependencyInjection,
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
                    someDependencyInjection,
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
                    someDependencyInjection,
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
                    someDependencyInjection,
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
                    someDependencyInjection,
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
                    someDependencyInjection,
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
                    someDependencyInjection,
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

        public static TheoryData<DependencyInjection, Xeption> BuildServiceProviderValidationExceptions()
        {
            return new TheoryData<DependencyInjection, Xeption>
            {
                {
                    null,
                    CreateInvalidDependencyInjectionParameterException(
                        new Dictionary<string, string>
                        {
                            {nameof(DependencyInjection), "object is required" }
                        })
                },

                {
                    new DependencyInjection(),
                    CreateInvalidServiceCollectionParameterException(
                        new Dictionary<string, string>
                        {
                            {nameof(ServiceCollection), "object is required" }
                        })
                },
            };
        }

        public static TheoryData<DependencyInjection, Xeption> GetServiceValidationExceptions()
        {
            return new TheoryData<DependencyInjection, Xeption>
            {
                {
                    null,
                    CreateInvalidDependencyInjectionParameterException(
                        new Dictionary<string, string>
                        {
                            {nameof(DependencyInjection), "object is required" }
                        })
                },

                {
                    new DependencyInjection(),
                    CreateInvalidServiceProviderParameterException(
                        new Dictionary<string, string>
                        {
                            {nameof(ServiceProvider), "object is required" }
                        })
                },
            };
        }

        public static TheoryData<DependencyInjection, string, Xeption> GetServiceWithSpalValidationExceptions()
        {
            return new TheoryData<DependencyInjection, string, Xeption>
            {
                {
                    null,
                    null,
                    CreateInvalidDependencyInjectionParameterException(
                        new Dictionary<string, string>
                        {
                            {nameof(DependencyInjection), "object is required" }
                        })
                },

                {
                    new DependencyInjection(),
                    null,
                    CreateInvalidServiceProviderParameterException(
                        new Dictionary<string, string>
                        {
                            {nameof(ServiceProvider), "object is required" }
                        })
                },

                {
                    new DependencyInjection
                    {
                        ServiceCollection = new ServiceCollection(),
                        ServiceProvider = new ServiceCollection().BuildServiceProvider()
                    },
                    null,
                    CreateInvalidServiceProviderParameterException(
                        new Dictionary<string, string>
                        {
                            {"spalI", "Value is required" }
                        })
                },
            };
        }
    }
}
