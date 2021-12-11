using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;

namespace Automation.Library.Infrastructure.AppEngine
{
    public interface IAppEngine
    {
        Container IocContainer { get; }

        T Resolve<T>(bool returnDefaultIfNotResolved = false) where T : class;

        T RegisterAndResolve<T>(object instance, bool instantiateIfNull = true, IReuse reuse = null) where T : class;

        void Start(bool testMode = false);
    }
}
