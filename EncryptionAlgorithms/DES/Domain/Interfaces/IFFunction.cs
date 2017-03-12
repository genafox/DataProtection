using System.Collections;

namespace DES.Domain.Interfaces
{
	public interface IFFunction
	{
		BitArray Invoke(BitArray rightPart, BitArray key);
	}
}
