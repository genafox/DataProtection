using System.Collections;
using System.Collections.Generic;
using DES.Misc;

namespace DES.Domain.Key
{
    public class CompressedPermutedKeyFactory
    {
        private const int RoundsCount = 16;

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

        public IDictionary<int, CompressedPermutedKey> Generate(BitArray originalKey)
        {
            var resultKeys = new Dictionary<int, CompressedPermutedKey>();

            var keysHalves = new Dictionary<int, HalvesBlock>
            {
                [0] = new InitialPermutedKey(originalKey).Halves
            };

            for (int i = 1; i <= RoundsCount; i++)
            {
                var c = keysHalves[i - 1].Left.CycleLeftShift(LeftShiftTable[i]);
                var d = keysHalves[i - 1].Right.CycleLeftShift(LeftShiftTable[i]);

                var compressedKey = new CompressedPermutedKey(c, d);

                keysHalves.Add(i, compressedKey.SourceHalves);
                resultKeys.Add(i, compressedKey);
            }

            return resultKeys;
        }
    }
}
