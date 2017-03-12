using Autofac;
using Common.LogDecorators;
using Contracts.Interfaces;
using DES;
using DES.Domain;
using DES.Domain.Interfaces;
using DES.Domain.Key;
using DES.Domain.SBox;
using DES.LogDecorators;
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

			containerBuilder.RegisterType<SBoxAddressFactory>()
				.AsSelf();

			containerBuilder.RegisterType<SBoxFunction>()
			   .AsSelf();

			containerBuilder.RegisterType<DesEncryptor>()
				.Named<IDataEncryptor>("dataEncryptors");

			containerBuilder.RegisterType<DesAlgorithm>()
				.Named<IDesAlgorithm>("desAlgorithm");

			containerBuilder.RegisterType<CompressedPermutedKeyFactory>()
				.Named<ICompressedPermutedKeyFactory>("desKeysFactory");

			containerBuilder.RegisterType<FFunction>()
				.Named<IFFunction>("desFFunction");

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

			containerBuilder
				.RegisterDecorator<IDesAlgorithm>(
					(c, inner) => new DesAlgorithmLogDecorator(inner, c.Resolve<ILogger>()),
					fromKey: "desAlgorithm")
				.As<IDesAlgorithm>();

			containerBuilder
				.RegisterDecorator<ICompressedPermutedKeyFactory>(
					(c, inner) => new CompressedPermutedKeyFactoryLogDecorator(inner, c.Resolve<ILogger>()), 
					fromKey: "desKeysFactory")
				.As<ICompressedPermutedKeyFactory>();

			containerBuilder
				.RegisterDecorator<IFFunction>(
					(c, inner) => new FFunctionLogDecorator(inner, c.Resolve<ILogger>()), 
					fromKey: "desFFunction")
				.As<IFFunction>();


		}
	}
}
