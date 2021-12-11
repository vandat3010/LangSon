using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using DryIoc;
using DryIoc.Mvc;
using CDKTTCTN.Core.Infrastructure.DependencyManagement;
using CDKTTCTN.Core.Infrastructure.Utils;
using CDKTTCTN.Core.Startup;

namespace Automation.Library.Infrastructure.AppEngine
{
    
    public class EngineContext : IAppEngine
    {
        public Container IocContainer { get; private set; }


        public T Resolve<T>(bool returnDefaultIfNotResolved = false) where T : class
        {
            return IocContainer.Resolve<T>(returnDefaultIfNotResolved ? IfUnresolved.ReturnDefault : IfUnresolved.Throw);
        }

        public T RegisterAndResolve<T>(object instance = null, bool instantiateIfNull = true, IReuse reuse = null) where T : class
        {
            if (instance == null)
                if (instantiateIfNull)
                    instance = Activator.CreateInstance<T>();
                else
                {
                    throw new Exception("Can't register a null instance");
                }
            var typedInstance = Resolve<T>(true);
            if (typedInstance == null)
            {
                IocContainer.RegisterInstance<T>(instance as T, reuse, ifAlreadyRegistered: IfAlreadyRegistered.Replace);
                typedInstance = instance as T;
            }
            return typedInstance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Start(bool testMode = false)
        {
            //setup ioc container
            SetupContainer();

            //setup dependency resolvers
            SetupDependencies(testMode);

            if (!testMode)
            {
                //run startup tasks
                RunStartupTasks();

                //start task manager to run scheduled tasks
                //StartTaskManager();
            }

        }

        public void SetupContainer()
        {
            IocContainer = new Container(rules => rules.WithoutThrowIfDependencyHasShorterReuseLifespan(), new AsyncExecutionFlowScopeContext());
        }

        private void SetupDependencies(bool testMode = false)
        {
            //first the self
            IocContainer.Register<IAppEngine, EngineContext>(Reuse.Singleton);

            //now the other dependencies by other plugins or system
            var dependencies = TypeFinder.ClassesOfType<IDependencyRegistrar>();
            //create instances for them
            var dependencyInstances = dependencies.Select(dependency => (IDependencyRegistrar)Activator.CreateInstance(dependency)).ToList();
            //reorder according to priority
            dependencyInstances = dependencyInstances.OrderBy(x => x.Priority).ToList();

            foreach (var di in dependencyInstances)
                //register individual instances in that order
                di.RegisterDependencies(IocContainer);

            //and it's resolver
            if (!testMode)
                IocContainer.WithMvc();
        }

        private void RunStartupTasks()
        {
            var startupTasks = TypeFinder.ClassesOfType<IStartupTask>();
            var tasks =
                startupTasks.Select(startupTask => (IStartupTask)Activator.CreateInstance(startupTask)).ToList();

            //reorder according to prioiryt
            tasks = tasks.OrderBy(x => x.Priority).ToList();

            foreach (var task in tasks)
                task.Run();            
        }
       

        public static EngineContext ActiveEngine
        {
            get { return Singleton<EngineContext>.Instance; }
        }
    }
}
