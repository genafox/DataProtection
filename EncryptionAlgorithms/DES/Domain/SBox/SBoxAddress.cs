using System;
using System.Collections;

namespace DES.Domain.SBox
{
	public class SBoxAddress
	{
		private readonly int rowIndex;
		private readonly int columnIndex;

		public SBoxAddress(BitArray address)
		{
			string binaryRowIndex = ToBinaryString(address[0]) + ToBinaryString(address[address.Length - 1]);
			this.rowIndex = Convert.ToInt32(binaryRowIndex, 2);

			string binaryColumnIndex = ToBinaryString(address[0]) + ToBinaryString(address[address.Length - 1]);
			this.columnIndex = Convert.ToInt32(binaryColumnIndex, 2);
		}

		public int Column => this.columnIndex;

		public int Row => this.rowIndex;

		private static string ToBinaryString(bool value)
		{
			return value ? "1" : "0";
		}
	}
}
