using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DES.Misc;

namespace DES.Infrastructure
{
    public class ParsedToken
    {
		private const int BytesIn64Bits = 8;

		public ParsedToken(string message, Encoding encoding = null)
        {
			this.Encoding = encoding ?? Encoding.Default;

            byte[] messageBytes = this.Encoding.GetBytes(message);

            this.OriginalBytesCount = messageBytes.Length;

            messageBytes = CompleteTo64BitsBlocks(messageBytes);

            this.BitBlocks = messageBytes.Get64BitsBlocks();
        }

        public int OriginalBytesCount { get; }

        public IEnumerable<BitArray> BitBlocks { get; }

		public Encoding Encoding { get; }

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
