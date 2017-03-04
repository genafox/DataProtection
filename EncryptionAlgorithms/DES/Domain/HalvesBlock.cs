using System.Collections;
using DES.Misc;

namespace DES.Domain
{
    public class HalvesBlock
    {
        public HalvesBlock(BitArray leftPart, BitArray rightPart)
        {
            this.Left = leftPart;
            this.Right = rightPart;
        }

        public HalvesBlock(BitArray source)
        {
            this.Left = source.GetRange(0, source.Length / 2);
            this.Right = source.GetRange(source.Length / 2, source.Length / 2);
        }

        public BitArray Left { get; }

        public BitArray Right { get; }
    }
}
