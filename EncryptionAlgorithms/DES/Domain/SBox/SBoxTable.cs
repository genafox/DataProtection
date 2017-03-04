using System.Collections.Generic;

namespace DES.Domain.SBox
{
    public class SBoxTable
    {
        private readonly Dictionary<int, int[]> table;

        public SBoxTable(Dictionary<int, int[]> table)
        {
            this.table = table;
        }

        public int GetItem(SBoxAddress address)
        {
            return this.table[address.Row][address.Column];
        }
    }
}
