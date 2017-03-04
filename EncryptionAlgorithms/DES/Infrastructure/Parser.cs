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

        public IEnumerable<BitArray> Parse(string message)
        {
            byte[] messageBytes = Encoding.Default.GetBytes(message);

            messageBytes = CompleteTo64BitsBlocks(messageBytes);

            var result = new List<BitArray>();

            int skip = 0;
            while (skip < messageBytes.Length)
            {
                byte[] block = messageBytes.Skip(skip).Take(BytesIn64Bits).ToArray();
                result.Add(new BitArray(block));

                skip += BytesIn64Bits;
            }

            return result;
        }

        public string GetHexString(IEnumerable<BitArray> bytesBlocks)
        {
            BitArray messageBits = new BitArray(new bool[0]).Concat(bytesBlocks.ToArray());

            byte[] messageBytes = new byte[messageBits.Length / BitsInByte];
            messageBits.CopyTo(messageBytes, 0);

            string result = BitConverter.ToString(messageBytes);

            return result.Replace("-", "");
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
