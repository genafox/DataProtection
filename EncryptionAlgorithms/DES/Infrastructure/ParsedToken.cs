using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DES.Infrastructure
{
    public class ParsedToken
    {
		private const int BytesIn64Bits = 8;
		private const int BitsInByte = 8;

		public ParsedToken(string message, Encoding encoding = null)
        {
			this.Encoding = encoding ?? Encoding.UTF8;
            OriginalBytesCount = originalBytesCount;
            BitBlocks = extractedBits;
        }

        public int OriginalBytesCount { get; }

        public IEnumerable<BitArray> BitBlocks { get; }

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
