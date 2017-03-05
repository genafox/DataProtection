using System.Collections;
using System.Collections.Generic;

namespace DES.Infrastructure
{
    public class ParsedToken
    {
        public ParsedToken(int originalBytesCount, IEnumerable<BitArray> extractedBits)
        {
            OriginalBytesCount = originalBytesCount;
            ExtractedBits = extractedBits;
        }

        public int OriginalBytesCount { get; }

        public IEnumerable<BitArray> ExtractedBits { get; }
    }
}
