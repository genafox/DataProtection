using System.Collections;

namespace DES.Domain.Block
{
    public class PermutedBlock
    {
        public PermutedBlock(BitArray left, BitArray right)
        {
            this.Left = left;
            this.Right = right;
        }

        public BitArray Left { get; }

        public BitArray Right { get; }
    }
}
