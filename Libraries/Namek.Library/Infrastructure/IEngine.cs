using System;
using Namek.Library.Infrastructure.DependencyManagement;

namespace Namek.Library.Infrastructure
{
    /// <summary>
    ///     Classes implementing this interface can serve as a portal for the
    ///     various services composing the engine. Edit functionality, modules
    ///     and implementations access most functionality through this
    ///     interface.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        ///     Container manager
        /// </summary>
        ContainerManager ContainerManager { get; }

        /// <summary>
        ///     Initialize components and plugins in the nop environment.
        /// </summary>
        /// <param name="config">Config</param>
        void Initialize(NopConfig config);

        /// <summary>
        ///     Resolve dependency
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        T Resolve<T>() where T : class;

        /// <summary>
        ///     Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        ///     Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        T[] ResolveAll<T>();
    }
}