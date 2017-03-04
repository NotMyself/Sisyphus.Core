using Autofac;
using Sisyphus.Core;

namespace Sisyphus.Jobs.Example
{
    public class ExampleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExampleJob>()
                .AsSelf()
                .As<IBackgroundJob>()
                .As<IBackgroundJobScheduler>();
        }
    }
}