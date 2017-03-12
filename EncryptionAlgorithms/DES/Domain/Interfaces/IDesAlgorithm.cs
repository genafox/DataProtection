using System.Collections;

namespace DES.Domain.Interfaces
{
	public interface IDesAlgorithm
	{
		BitArray Encrypt(BitArray originalBlock, BitArray originalKey);

		BitArray Decrypt(BitArray encryptedBlock, BitArray originalKey);
	}
}
