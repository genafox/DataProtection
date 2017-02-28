using System.Collections;

namespace DES.Domain.Key
{
    public class PermutedKey
    {
        public PermutedKey(BitArray left, BitArray right, BitArray compressedResult)
        {
            this.Left = left;
            this.Right = right;
            this.CompressedResult = compressedResult;
        }

        public BitArray Left { get; }

        public BitArray Right { get; }

        public BitArray CompressedResult { get; }
    }
}
