using System.Collections;
using System.Collections.Generic;
using DES.Misc;

namespace DES.Domain.Key
{
    public class PermutedKeyFactory
    {
        private const int RoundsCount = 16;

        private static readonly int[] PermutationTable =
        {
            57, 49, 41, 33, 25, 17, 9,
            1, 58, 50, 42, 34, 26, 18,
            10,  2, 59, 51, 43, 35, 27,
            19, 11, 3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
            7, 62, 54, 46, 38, 30, 22,
            14, 6, 61, 53, 45, 37, 29,
            21, 13, 5, 28, 20, 12, 4
        };

        private static readonly Dictionary<int, int> LeftShiftTable = new Dictionary<int, int>
        {
            [1] = 1,
            [2] = 1,
            [3] = 2,
            [4] = 2,
            [5] = 2,
            [6] = 2,
            [7] = 2,
            [8] = 2,
            [9] = 1,
            [10] = 2,
            [11] = 2,
            [12] = 2,
            [13] = 2,
            [14] = 2,
            [15] = 2,
            [16] = 1
        };

        private static readonly int[] CompressedPermutationTable =
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

        public IDictionary<int, PermutedKey> Generate(BitArray originalKey)
        {
            var result = new Dictionary<int, PermutedKey>();

            var permutedKey = PerformPermutation(originalKey);
            var zeroKey = new PermutedKey(
                permutedKey.GetRange(0, permutedKey.Length / 2),
                permutedKey.GetRange(permutedKey.Length / 2, permutedKey.Length / 2),
                new BitArray(0));

            result.Add(0, zeroKey);

            for (int i = 1; i <= RoundsCount; i++)
            {
                var c = result[i - 1].Left.CycleLeftShift(LeftShiftTable[i]);
                var d = result[i - 1].Right.CycleLeftShift(LeftShiftTable[i]);
                BitArray compressedResult = PerformCompressedPermutation(c.Concat(d));

                result.Add(i, new PermutedKey(c, d, compressedResult));
            }

            result.Remove(0);

            return result;
        }

        private static BitArray PerformPermutation(BitArray source)
        {
            var result = new BitArray(PermutationTable.Length);

            for (int i = 0; i < PermutationTable.Length; i++)
            {
                result[i] = source[PermutationTable[i]];
            }

            return result;
        }

        private static BitArray PerformCompressedPermutation(BitArray source)
        {
            var result = new BitArray(CompressedPermutationTable.Length);

            for (int i = 0; i < CompressedPermutationTable.Length; i++)
            {
                result[i] = source[CompressedPermutationTable[i]];
            }

            return result;
        }
    }
}
