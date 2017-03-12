using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Extensions;

namespace DES.Domain.Tokens
{
    public class DecryptedToken
    {
        private const int BitsInByte = 8;

        private readonly int originalBytesCount;

        private DecryptedToken(Encoding encoding = null)
        {
            this.Encoding = encoding ?? Encoding.Default;
        }

        public DecryptedToken(int originalBytesCount, IEnumerable<BitArray> decryptedBitBlocks, Encoding encoding = null) : this(encoding)
        {
            this.originalBytesCount = originalBytesCount;
            this.DecryptedBitBlocks = decryptedBitBlocks;
        }

        public Encoding Encoding { get; }

        public IEnumerable<BitArray> DecryptedBitBlocks { get; }

        public override string ToString()
        {
            BitArray decryptedBits = new BitArray(new bool[0]).Concat(this.DecryptedBitBlocks.ToArray());

            byte[] decryptedBytes = new byte[decryptedBits.Length / BitsInByte];
            decryptedBits.CopyTo(decryptedBytes, 0);

            string decryptedString = this.Encoding.GetString(decryptedBytes, 0, this.originalBytesCount);

            return decryptedString;
        }
    }
}
