using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DES.Domain.DataBlock;
using DES.Domain.Key;

namespace DES.Domain
{
    public class Encryptor
    {
        private const int RoundsCount = 16;

        private readonly FFunction ffunction;
        private readonly CompressedPermutedKeyFactory keyFactory;

        public Encryptor(FFunction ffunction, CompressedPermutedKeyFactory keyFactory)
        {
            this.ffunction = ffunction;
            this.keyFactory = keyFactory;
        }

        public BitArray Encrypt(BitArray originalBlock, BitArray originalKey)
        {
            var iterations = new Dictionary<int, HalvesBlock>
            {
                [0] = new InitialPermutedDataBlock(originalBlock).Halves
            };

            IDictionary<int, CompressedPermutedKey> keys = keyFactory.Generate(originalKey);

            for (int i = 1; i <= RoundsCount; i++)
            {
                HalvesBlock previousBlock = iterations[i - 1];
                BitArray currentKey = keys[i].CompressedValue;

                var left = previousBlock.Right;
                var right = previousBlock.Left.Xor(this.ffunction.Invoke(previousBlock.Right, currentKey));

                iterations.Add(i, new HalvesBlock(left, right));
            }

            BitArray encrypted = new FinalPermutedDataBlock(iterations.Last().Value).Value;

            return encrypted;
        }
    }
}
