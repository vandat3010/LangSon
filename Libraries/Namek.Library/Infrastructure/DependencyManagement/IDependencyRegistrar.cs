using Autofac;

namespace Namek.Library.Infrastructure.DependencyManagement
{
    /// <summary>
    ///     Dependency registrar interface
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        ///     Order of this dependency registrar implementation
        /// </summary> 

        /// <summary>
        ///     Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config);
    }
}