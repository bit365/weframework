using Unity.Lifetime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Resolution;

namespace WeFramework.Core.Infrastructure
{
    public static class UnityExtensions
    {
        /// <summary>
        /// Utility method to try to resolve a service from the container avoiding an exception if the container cannot build the type.
        /// </summary>
        /// <param name="container">The cointainer that will be used to resolve the type.</param>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>The instance of <typeparamref name="T"/> built up by the container.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static T TryResolve<T>(this IUnityContainer container)
        {
            object result = TryResolve(container, typeof(T));
            if (result != null)
            {
                return (T)result;
            }
            return default(T);
        }

        /// <summary>
        /// Utility method to try to resolve a service from the container avoiding an exception if the container cannot build the type.
        /// </summary>
        /// <param name="container">The cointainer that will be used to resolve the type.</param>
        /// <param name="typeToResolve">The type to resolve.</param>
        /// <returns>The instance of <paramref name="typeToResolve"/> built up by the container.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static object TryResolve(this IUnityContainer container, Type typeToResolve, params ResolverOverride[] resolverOverrides)
        {
            return container.TryResolve(typeToResolve, null, resolverOverrides);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static object TryResolve(this IUnityContainer container, Type typeToResolve, string name, params ResolverOverride[] resolverOverrides)
        {
            try
            {
                return container.Resolve(typeToResolve, name, resolverOverrides);
            }
            catch
            {
                return null;
            }
        }
    }
}
