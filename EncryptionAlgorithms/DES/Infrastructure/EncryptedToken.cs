using System.Collections;
using System.Collections.Generic;

namespace DES.Infrastructure
{
    public class EncryptedToken : ParsedToken
    {
        public EncryptedToken(int originalBytesCount, IEnumerable<BitArray> extractedBits) : base(originalBytesCount, extractedBits)
        {
        }
    }
}
