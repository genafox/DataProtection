using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DES.Misc;
using System.Text;

namespace DES.Infrastructure
{
    public class EncryptedToken
    {
        private const int BitsInByte = 8;
        private const char TechnicalInfoDelimeter = '+';

        private EncryptedToken(Encoding encoding = null)
        {
            this.Encoding = encoding ?? Encoding.Default;
        }

        public EncryptedToken(int originalBytesCount, IEnumerable<BitArray> encryptedBitBlocks, Encoding encoding = null) : this(encoding)
        {
            this.OriginalBytesCount = originalBytesCount;
            this.EncryptedBitBlocks = encryptedBitBlocks;
        }

        public EncryptedToken(string encryptedMessage, Encoding encoding = null) : this(encoding)
        {
            string[] splitted = encryptedMessage.Split(TechnicalInfoDelimeter);

            this.OriginalBytesCount = Convert.ToInt32(splitted[1]);

            byte[] encryptedBytes = splitted[0].GetBytesFromHex();

            this.EncryptedBitBlocks = encryptedBytes.Get64BitsBlocks();
        }

        public Encoding Encoding { get; }

        public int OriginalBytesCount { get; }

        public IEnumerable<BitArray> EncryptedBitBlocks { get; }

        public override string ToString()
        {
            BitArray encryptedBits = new BitArray(new bool[0]).Concat(this.EncryptedBitBlocks.ToArray());

            byte[] encryptedBytes = new byte[encryptedBits.Length / BitsInByte];
            encryptedBits.CopyTo(encryptedBytes, 0);

            string encryptedString = BitConverter.ToString(encryptedBytes).Replace("-", "");

            encryptedString = encryptedString + TechnicalInfoDelimeter + this.OriginalBytesCount;

            return encryptedString;
        }
    }
}
