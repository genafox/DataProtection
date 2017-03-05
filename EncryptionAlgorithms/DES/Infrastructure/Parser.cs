using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DES.Misc;

namespace DES.Infrastructure
{
    public class Parser
    {
        private const int BytesIn64Bits = 8;
        private const int BitsInByte = 8;

        public Parser(Encoding encoding = null)
        {
            Encoding = encoding ?? Encoding.UTF8;
        }

        public Encoding Encoding { get; }

        public ParsedToken ParseText(string message)
        {
            byte[] messageBytes = this.Encoding.GetBytes(message);
            int originalBytesCount = messageBytes.Length;

            messageBytes = CompleteTo64BitsBlocks(messageBytes);
            var bitsBlocks = new List<BitArray>();

            int skip = 0;
            while (skip < messageBytes.Length)
            {
                byte[] block = messageBytes.Skip(skip).Take(BytesIn64Bits).ToArray();
                bitsBlocks.Add(new BitArray(block));

                skip += BytesIn64Bits;
            }

            return new ParsedToken(originalBytesCount, bitsBlocks);
        }

        public ParsedToken ParseEncryptedText(string encryptedMessage)
        {
            byte[] messageBytes = this.Encoding.GetBytes(encryptedMessage);
            int originalBytesCount = messageBytes.Length;

            messageBytes = CompleteTo64BitsBlocks(messageBytes);
            var bitsBlocks = new List<BitArray>();

            int skip = 0;
            while (skip < messageBytes.Length)
            {
                byte[] block = messageBytes.Skip(skip).Take(BytesIn64Bits).ToArray();
                bitsBlocks.Add(new BitArray(block));

                skip += BytesIn64Bits;
            }

            return new ParsedToken(originalBytesCount, bitsBlocks);
        }

        public string GetDecryptedText(ParsedToken token)
        {
            BitArray messageBits = new BitArray(new bool[0]).Concat(token.BitBlocks.ToArray());

            byte[] messageBytes = new byte[messageBits.Length / BitsInByte];
            messageBits.CopyTo(messageBytes, 0);

            string result = Encoding.UTF8.GetString(messageBytes, 0, token.OriginalBytesCount);

            return result;
        }

        private byte[] CompleteTo64BitsBlocks(byte[] messageBytes)
        {
            if (messageBytes.Length % BytesIn64Bits > 0)
            {
                int missedBytesCount = BytesIn64Bits - messageBytes.Length % BytesIn64Bits;

                var bytesBlocks = new byte[messageBytes.Length + missedBytesCount];
                Array.Copy(messageBytes, bytesBlocks, messageBytes.Length);
                Array.Copy(new byte[missedBytesCount], 0, bytesBlocks, messageBytes.Length, missedBytesCount);

                return bytesBlocks;
            }

            return messageBytes;
        }
    }
}
