using System.Collections;
using System.Linq;
using DES.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DES
{
    [TestClass]
    public class ParsedTokenTests
    {
        private const int BytesIn64Bits = 8;

        [TestMethod]
        public void Constructor_CorrectSecuenceOfBitsIsExstractedFromString()
        {
            string originalMessage = "hello world";
            byte[] originalMessageBytes = { 104, 101, 108, 108, 111, 32, 119, 111, 114, 108, 100, 0, 0, 0, 0, 0 };

            BitArray[] result = new ParsedToken(originalMessage).BitBlocks.ToArray();

            CollectionAssert.AreEqual(
                new BitArray(originalMessageBytes.Take(BytesIn64Bits).ToArray()),
                result[0]);

            CollectionAssert.AreEqual(
                new BitArray(originalMessageBytes.Skip(BytesIn64Bits).Take(BytesIn64Bits).ToArray()),
                result[1]);
        }
    }
}
