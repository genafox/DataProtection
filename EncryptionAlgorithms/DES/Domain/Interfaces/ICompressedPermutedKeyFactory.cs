using System.Collections;
using System.Collections.Generic;
using DES.Domain.Key;

namespace DES.Domain.Interfaces
{
	public interface ICompressedPermutedKeyFactory
	{
		IDictionary<int, CompressedPermutedKey> Generate(BitArray originalKey);
	}
}
