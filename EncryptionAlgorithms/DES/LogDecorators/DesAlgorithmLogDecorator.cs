using System;
using System.Collections;
using Contracts.Interfaces;
using DES.Domain.Interfaces;

namespace DES.LogDecorators
{
	public class DesAlgorithmLogDecorator : IDesAlgorithm
	{
		private const string TabLevel = "\t";

		private readonly IDesAlgorithm algorithm;
		private readonly ILogger logger;

		public DesAlgorithmLogDecorator(IDesAlgorithm algorithm, ILogger logger)
		{
			this.algorithm = algorithm;
			this.logger = logger;
		}

		public BitArray Encrypt(BitArray originalBlock, BitArray originalKey)
		{
			Type decoratedType = this.algorithm.GetType();
			this.logger.LogInfo(TabLevel + $"{decoratedType.FullName}.Encrypt - Start");

			BitArray result = this.algorithm.Encrypt(originalBlock, originalKey);

			this.logger.LogInfo(TabLevel + $"{decoratedType.FullName}.Encrypt - End\r\n");

			return result;
		}

		public BitArray Decrypt(BitArray encryptedBlock, BitArray originalKey)
		{
			Type decoratedType = this.algorithm.GetType();
			this.logger.LogInfo(TabLevel + $"{decoratedType.FullName}.Decrypt - Start");

			BitArray result = this.algorithm.Decrypt(encryptedBlock, originalKey);

			this.logger.LogInfo(TabLevel + $"{decoratedType.FullName}.Decrypt - End\r\n");
			return result;
		}
	}
}
