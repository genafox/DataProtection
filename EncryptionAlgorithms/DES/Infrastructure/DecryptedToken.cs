using System.Collections;
using System.Collections.Generic;

namespace DES.Infrastructure
{
    public class DecryptedToken : ParsedToken
    {
        public DecryptedToken(int originalBytesCount, IEnumerable<BitArray> extractedBits) : base(originalBytesCount, extractedBits)
        {
        }
    }
}
