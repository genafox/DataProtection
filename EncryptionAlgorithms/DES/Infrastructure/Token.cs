using System.Collections;
using System.Collections.Generic;

namespace DES.Infrastructure
{
    public class Token
    {
        public Token(int originalBytesCount, IEnumerable<BitArray> extractedBits)
        {
            OriginalBytesCount = originalBytesCount;
            BitBlocks = extractedBits;
        }

        public int OriginalBytesCount { get; }

        public IEnumerable<BitArray> BitBlocks { get; }
    }
}
