using System.Collections;
using Common.Extensions;

namespace DES.Domain.Key
{
    public class CompressedPermutedKey
    {
        private static readonly int[] PermutationTable =
        {
            14, 17, 11, 24, 1, 5,
            3, 28, 15, 6, 21, 10,
            23, 19, 12, 4, 26, 8,
            16,  7, 27, 20, 13, 2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32
        };

        public CompressedPermutedKey(BitArray leftSource, BitArray rightSource)
        {
            this.SourceHalves = new HalvesBlock(leftSource, rightSource);
            this.CompressedValue = leftSource
                .Concat(rightSource)
                .Transform(PermutationTable, tableStartIndex: 1);
        }

        public HalvesBlock SourceHalves { get; }

        public BitArray CompressedValue { get; }
    }
}
