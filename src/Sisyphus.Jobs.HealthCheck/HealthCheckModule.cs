using Autofac;
using Sisyphus.Core;

namespace Sisyphus.Jobs.HealthCheck
{
    public class HealthCheckModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HealthCheck>()
                .AsSelf()
                .As<IBackgroundJob>()
                .As<IBackgroundJobScheduler>();
        }
    }
}