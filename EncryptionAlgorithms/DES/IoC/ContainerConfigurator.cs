using Autofac;

namespace DES.IoC
{
    public static class ContainerConfigurator
    {
        public static IContainer GetContainer()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterComponents(containerBuilder);

            return containerBuilder.Build();
        }

        public static void RegisterComponents(ContainerBuilder containerBuilder)
        {
            // Core Types
            containerBuilder.RegisterAssemblyTypes(typeof(DesEncryptor).Assembly)
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
