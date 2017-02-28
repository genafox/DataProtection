using System.Collections;

namespace DES.Domain
{
	public class FFunction
	{
		private static readonly int[] ExtensionSelectionTable =
		{
			32, 1, 2, 3, 4, 5,
			4, 5, 6, 7, 8, 9,
			8, 9, 10, 11, 12, 13,
			12, 13, 14, 15, 16, 17,
			16, 17, 18, 19, 20, 21,
			20, 21, 22, 23, 24, 25,
			24, 25, 26, 27, 28, 29,
			28, 29, 30, 31, 32, 1
		};

		public BitArray Invoke(BitArray rightPart, BitArray key)
		{
			var extendedRightPart = this.PerformExtension(rightPart);

			var multopliedKeyPart = key.Xor(extendedRightPart);

		}

		private BitArray PerformExtension(BitArray source)
		{
			var result = new BitArray(ExtensionSelectionTable.Length);

			for (int i = 0; i < ExtensionSelectionTable.Length; i++)
			{
				result[i] = source[ExtensionSelectionTable[i]];
			}

			return result;
		}
	}
}
