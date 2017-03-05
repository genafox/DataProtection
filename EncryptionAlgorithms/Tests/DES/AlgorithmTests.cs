using System.Collections;
using System.Linq;
using System.Text;
using DES.Domain;
using DES.Domain.Key;
using DES.Domain.SBox;
using DES.Infrastructure;
using DES.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.DES.Utils;

namespace Tests.DES
{
    [TestClass]
    public class AlgorithmTests
    {
        [TestMethod]
        public void Encrypt_WhenDataAndKeyAreValid_ReturnsEncryptedData()
        {
            string originalMessage = "hello world";
            var parsedToken = new ParsedToken(originalMessage);

            var keyBits = new BitArray("13-34-57-79-9B-BC-DF-F1".GetBytesFromHex());

            Algorithm algorithm = FakeObjectsFactory.CreateAlgorithm();

            BitArray[] encryptedResult = parsedToken.BitBlocks.Select(block => algorithm.Encrypt(block, keyBits)).ToArray();
            BitArray[] decryptedResult = encryptedResult.Select(block => algorithm.Decrypt(block, keyBits)).ToArray();

            var decryptedToken = new DecryptedToken(parsedToken.OriginalBytesCount, decryptedResult);

            Assert.AreEqual(originalMessage, decryptedToken.ToString());
        }
    }
}
