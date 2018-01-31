using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WeFramework.Core.Infrastructure
{
    /// <summary>
    /// A class that finds types needed by Nop by looping assemblies in the 
    /// currently executing AppDomain. Only assemblies whose names matches
    /// certain patterns are investigated and an optional list of assemblies
    /// referenced by <see cref="AssemblyNames"/> are always investigated.
    /// </summary>
    public class AppDomainTypeFinder : ITypeFinder
    {
        #region Fields

        private readonly string ignorePattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper";

        private readonly string filterPattern = ".*";


        #endregion

        #region Methods

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            List<Type> types = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (assignTypeFrom.IsAssignableFrom(type) || (assignTypeFrom.IsGenericTypeDefinition && IsAssignableGeneric(type, assignTypeFrom)))
                    {
                        if (!type.IsInterface)
                        {
                            if (onlyConcreteClasses)
                            {
                                if (type.IsClass && !type.IsAbstract)
                                {
                                    types.Add(type);
                                }
                            }
                            else
                            {
                                types.Add(type);
                            }
                        }
                    }

                }
            }

            return types;
        }

        /// <summary>Gets the assemblies related to the current implementation.</summary>
        /// <returns>A list of assemblies that should be loaded by the Nop factory.</returns>
        public virtual IList<Assembly> GetAssemblies()
        {
            return this.GetCurrentDomainAssemblies();
        }

        public IList<Assembly> GetCurrentDomainAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (Matches(assembly.FullName))
                {
                    if (!assemblies.Any(a => a.FullName.Contains(assembly.FullName)))
                    {
                        assemblies.Add(assembly);
                    }
                }
            }

            return assemblies;
        }

        /// <summary>
        /// Makes sure matching assemblies in the supplied folder are loaded in the app domain.
        /// </summary>
        /// <param name="directoryPath">
        /// The physical path to a directory containing dlls to load in the app domain.
        /// </param>
        protected virtual void LoadMatchingAssemblies(string directory)
        {
            var loadedAssemblyNames = this.GetCurrentDomainAssemblies().Select(a => a.FullName);

            if (Directory.Exists(directory))
            {
                foreach (string dllFilePath in Directory.GetFiles(directory, "*.dll"))
                {
                    try
                    {
                        var assemblyName = AssemblyName.GetAssemblyName(dllFilePath);
                        if (Matches(assemblyName.FullName) && !loadedAssemblyNames.Contains(assemblyName.FullName))
                        {
                            AppDomain.CurrentDomain.Load(assemblyName);
                        }
                    }
                    catch (BadImageFormatException ex)
                    {
                        Trace.TraceError(ex.ToString());
                    }
                }
            }
        }

        #endregion


        #region Utilities

        /// <summary>
        /// Check if a dll is one of the shipped dlls that we know don't need to be investigated.
        /// </summary>
        /// <param name="assemblyFullName">
        /// The name of the assembly to check.
        /// </param>
        /// <returns>
        /// True if the assembly should be loaded into Nop.
        /// </returns>
        public virtual bool Matches(string assemblyFullName)
        {
            bool isIgnore = Regex.IsMatch(assemblyFullName, ignorePattern, RegexOptions.IgnoreCase);
            bool isFilter = Regex.IsMatch(assemblyFullName, filterPattern, RegexOptions.IgnoreCase);

            return !isIgnore && isFilter;

        }

        /// <summary>
        /// Does type implement generic?
        /// </summary>
        /// <param name="type"></param>
        /// <param name="openGeneric"></param>
        /// <returns></returns>
        protected virtual bool IsAssignableGeneric(Type type, Type genericType)
        {
            var genericTypeDefinition = genericType.GetGenericTypeDefinition();

            foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
            {
                if (implementedInterface.IsGenericType)
                {
                    return genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                }
            }

            return false;
        }

        #endregion

    }
}
