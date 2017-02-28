using System.Collections;
using DES.Domain.Block;
using DES.Misc;

namespace DES.Domain
{
    public class Encryptor
    {
        private readonly PermutedBlockService permutedBlockService;

        public Encryptor(PermutedBlockService permutedBlockService)
        {
            this.permutedBlockService = permutedBlockService;
        }

        public BitArray Encrypt(BitArray originalBlock)
        {
            BitArray initialPermutation = this.permutedBlockService.InitialPermutation(originalBlock);

            var zeroBlock = new PermutedBlock(
                initialPermutation.GetRange(0, initialPermutation.Length / 2),
                initialPermutation.GetRange(initialPermutation.Length / 2, initialPermutation.Length / 2));


        }
    }
}
