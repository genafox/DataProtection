using Autofac;
using Common.LogDecorators;
using Contracts.Interfaces;
using DES;
using DES.Domain;
using DES.Domain.Key;
using DES.Domain.SBox;
using Logs.Loggers;

namespace IoC
{
    public static class ContainerInitializer
    {
        private const string LogFilePath = @"../../../Logs/log.txt";

        public static IContainer GetContainer()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterComponents(containerBuilder);

            return containerBuilder.Build();
        }

        private static void RegisterComponents(ContainerBuilder containerBuilder)
        {
            // DES components
            containerBuilder
                .RegisterType<CompressedPermutedKeyFactory>()
                .AsSelf();

            containerBuilder.RegisterType<SBoxAddressFactory>()
                .AsSelf();

            containerBuilder.RegisterType<SBoxFunction>()
               .AsSelf();

            containerBuilder.RegisterType<FFunction>()
                .AsSelf();

            containerBuilder.RegisterType<Algorithm>()
                .AsSelf();

            containerBuilder.RegisterType<DesEncryptor>()
                .Named<IDataEncryptor>("dataEncryptors");

            // Logs components
            containerBuilder.RegisterType<FileLogger>()
                .As<ILogger>()
                .WithParameter("logFilePath", LogFilePath);

            // Decorators
            containerBuilder
                .RegisterDecorator<IDataEncryptor>(
                    (c, inner) => new DataEncryptorLogDecorator(inner, c.Resolve<ILogger>()),
                    fromKey: "dataEncryptors")
                .As<IDataEncryptor>();
        }
    }
}
