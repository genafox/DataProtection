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

            BitArray[] result = new Parser().Parse(originalMessage).ExtractedBits.ToArray();

            CollectionAssert.AreEqual(
                new BitArray(originalMessageBytes.Take(BytesIn64Bits).ToArray()),
                result[0]);

            CollectionAssert.AreEqual(
                new BitArray(originalMessageBytes.Skip(BytesIn64Bits).Take(BytesIn64Bits).ToArray()),
                result[1]);
        }

        [TestMethod]
        public void GetString_ReturnsCorrectString()
        {
            var parser = new Parser();
            string originalMessage = "hello world";
            ParsedToken parsedMessage = parser.Parse(originalMessage);

            string result = parser.GetString(parsedMessage);

            Assert.AreEqual(originalMessage, result);
        }
    }
}
