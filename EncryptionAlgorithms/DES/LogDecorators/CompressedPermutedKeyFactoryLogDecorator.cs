using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Contracts.Interfaces;
using DES.Domain.Interfaces;
using DES.Domain.Key;

namespace DES.LogDecorators
{
	public class CompressedPermutedKeyFactoryLogDecorator : ICompressedPermutedKeyFactory
	{
		private const string TabLevel = "\t\t";
		private const string ChildTabLevel = "\t\t\t";

		private readonly ICompressedPermutedKeyFactory keyFactory;
		private readonly ILogger logger;

		public CompressedPermutedKeyFactoryLogDecorator(ICompressedPermutedKeyFactory keyFactory, ILogger logger)
		{
			this.keyFactory = keyFactory;
			this.logger = logger;
		}

		public IDictionary<int, CompressedPermutedKey> Generate(BitArray originalKey)
		{
			Type decoratedType = this.keyFactory.GetType();
			this.logger.LogInfo(TabLevel + $"{decoratedType.FullName}.Generate - Start");

			IDictionary<int, CompressedPermutedKey> result = this.keyFactory.Generate(originalKey);

			this.logger.LogInfo(
				TabLevel + "\t" + $"Compresed permuted keys: \r\n {GetKeysMessage(result)}",
				TabLevel + "\t\t" + $"{decoratedType.FullName}.Generate - End");

			return result;
		}

		private static string GetKeysMessage(IDictionary<int, CompressedPermutedKey> keys)
		{
			string[] messageParts = keys
				.Select(pair => $"Key {pair.Key}: {pair.Value.CompressedValue.ToBinaryString()}")
				.ToArray();

			string separator = "\r\n" + TabLevel + ChildTabLevel;

			return TabLevel + ChildTabLevel + string.Join(separator, messageParts);
		}
	}
}
