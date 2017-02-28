using System.Collections;
using System.Collections.Generic;

namespace DES.Domain.SBox
{
	public class SBoxTable
	{
		private readonly Dictionary<int, int[]> sFunctions;

		public SBoxTable(Dictionary<int, int[]> sFunctions)
		{
			this.sFunctions = sFunctions;
		}
	}
}
