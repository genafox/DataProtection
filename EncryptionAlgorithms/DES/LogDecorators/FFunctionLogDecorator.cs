using System;
using System.Collections;
using Common.Extensions;
using Contracts.Interfaces;
using DES.Domain.Interfaces;

namespace DES.LogDecorators
{
	public class FFunctionLogDecorator : IFFunction
	{
		private const string TabLevel = "\t\t";
		private const string ChildTabLevel = "\t\t\t";

		private readonly IFFunction ffunction;
		private readonly ILogger logger;

		public FFunctionLogDecorator(IFFunction ffunction, ILogger logger)
		{
			this.ffunction = ffunction;
			this.logger = logger;
		}

		public BitArray Invoke(BitArray rightPart, BitArray key)
		{
			Type decoratedType = this.ffunction.GetType();
			this.logger.LogInfo(TabLevel + $"{decoratedType.FullName}.Invoke - Start");

			BitArray result = this.ffunction.Invoke(rightPart, key);

			this.logger.LogInfo(
				TabLevel + "\t" + $"Key[i]: {key.ToBinaryString()}",
				TabLevel + ChildTabLevel + $"Left[i] = Right[i-1] = {rightPart.ToBinaryString()}",
				TabLevel + ChildTabLevel + $"F(Right[i-1], Key[i]) = {result.ToBinaryString()}",
				TabLevel + "\t\t" + $"{decoratedType.FullName}.Encrypt - End");

			return result;
		}
	}
}
