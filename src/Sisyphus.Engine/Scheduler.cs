using System.Collections.Generic;
using Autofac;
using Sisyphus.Core;

namespace Sisyphus.Engine
{
    public class Scheduler
    {
        public void Schedule(IContainer container)
        {
            if (container.IsRegistered<IEnumerable<IBackgroundJobScheduler>>())
                foreach (var scheduler in container.Resolve<IEnumerable<IBackgroundJobScheduler>>())
                    scheduler.Schedule();
        }
    }
}