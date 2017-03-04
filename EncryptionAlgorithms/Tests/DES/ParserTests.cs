using System.Collections;
using System.Linq;
using DES.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DES
{
    [TestClass]
    public class ParserTests
    {
        private const int BytesIn64Bits = 8;

        [TestMethod]
        public void Parse_ReturnsCorrectSecuenceOfBits()
        {
            string originalMessage = "hello world";
            byte[] originalMessageBytes = { 104, 101, 108, 108, 111, 32, 119, 111, 114, 108, 100, 0, 0, 0, 0, 0 };

            BitArray[] result = new Parser().Parse(originalMessage).ToArray();

            CollectionAssert.AreEqual(
                new BitArray(originalMessageBytes.Take(BytesIn64Bits).ToArray()),
                result[0]);

            CollectionAssert.AreEqual(
                new BitArray(originalMessageBytes.Skip(BytesIn64Bits).Take(BytesIn64Bits).ToArray()),
                result[1]);
        }

        [TestMethod]
        public void GetHexString_ReturnsCorrectHexString()
        {
           var originalMessageBits = new[]
            {
                new BitArray(new byte[] { 104, 101, 108, 108, 111, 32, 119, 111 }),
                new BitArray(new byte[] { 114, 108, 100, 0, 0, 0, 0, 0 })
            };
            string originalMessageHex = "68-65-6C-6C-6F-20-77-6F-72-6C-64-00-00-00-00-00".Replace("-", "");

            string result = new Parser().GetHexString(originalMessageBits);

            Assert.AreEqual(originalMessageHex, result);
        }
    }
}
