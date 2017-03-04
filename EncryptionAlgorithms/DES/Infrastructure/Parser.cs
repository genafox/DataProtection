using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DES.Misc;

namespace DES.Infrastructure
{
    public class Parser
    {
        private const int BytesIn64Bits = 8;

        private const int BitsInByte = 8;

        public IEnumerable<BitArray> FromString(string message)
        {
            byte[] messageBytes = Encoding.Default.GetBytes(message);

            List<string> messageHexPairs = BitConverter.ToString(messageBytes).Split('-').ToList();

            

            messageBytes = messageHexPairs
                .Select(x => Convert.ToByte(x, 16))
                .ToArray();

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

        private string[] CompleteTo64BitsBlocks(string[] hexPairs)
        {
            if (hexPairs.Length * BytesIn64Bits % 64 > 0)
            {
                // 1 hex pair (FF) = 1 byte (1111 1111)
                int filled64BitBlocksCount = hexPairs.Length * BitsInByte / 64;

                var result = new string[8 - (hexPairs.Length - filled64BitBlocksCount * BytesIn64Bits)];

                for (int i = hexPairs.Length; i < result.Length; i++)
                {
                    result[i] = "00";
                }
            }
        }
    }
}
