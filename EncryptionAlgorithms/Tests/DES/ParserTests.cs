using System.Collections;
using System.Linq;
using DES.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DES
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Parse_ReturnsListOf64BitArrays()
        {
            string originalMessage = "Your lips are smoother than vaseline";

            var result = new Parser().FromString(originalMessage).ToArray();

            CollectionAssert.AreEqual(result[0], new BitArray(1));
        }
    }
}
