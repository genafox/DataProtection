using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class ByteArrayExtensions
    {
        private const int BytesIn64Bits = 8;

        public static IEnumerable<BitArray> Get64BitsBlocks(this byte[] bytesArray)
        {
            var bitsBlocks = new List<BitArray>();

            int skip = 0;
            while (skip < bytesArray.Length)
            {
                byte[] block = bytesArray.Skip(skip).Take(BytesIn64Bits).ToArray();
                bitsBlocks.Add(new BitArray(block));

                skip += BytesIn64Bits;
            }

            return bitsBlocks;
        }
    }
}
