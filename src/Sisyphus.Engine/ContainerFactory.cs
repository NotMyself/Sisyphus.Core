using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace Sisyphus.Engine
{
    public static class ContainerFactory
    {
        private static IContainer Build()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("plugins.json");

            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();

            builder.RegisterModule(module);

            return builder.Build();
        }
    }
}
