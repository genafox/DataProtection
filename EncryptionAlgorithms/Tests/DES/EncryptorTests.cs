using System.Collections;
using System.Linq;
using DES.Domain;
using DES.Domain.Key;
using DES.Domain.SBox;
using DES.Infrastructure;
using DES.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DES
{
    [TestClass]
    public class EncryptorTests
    {
        [TestMethod]
        public void Encrypt_WhenDataAndKeyAreValid_ReturnsEncryptedData()
        {
            string originalMessage = "hello world";
            var parsedToken = new ParsedToken(originalMessage);

            var keyBits = new BitArray("13-34-57-79-9B-BC-DF-F1".GetBytesFromHex());

            Algorithm algorithm = CreateEncryptor();

            BitArray[] encryptedResult = parsedToken.BitBlocks.Select(block => algorithm.Encrypt(block, keyBits)).ToArray();
            BitArray[] decryptedResult = encryptedResult.Select(block => algorithm.Decrypt(block, keyBits)).ToArray();

            var decryptedToken = new DecryptedToken(parsedToken.OriginalBytesCount, decryptedResult);

            Assert.AreEqual(originalMessage, decryptedToken.ToString());
        }

        private static Algorithm CreateEncryptor()
        {
            var compressedKeyFactory = new CompressedPermutedKeyFactory();

            var sboxFunction = new SBoxFunction();
            var sboxAddressesFactory = new SBoxAddressFactory();
            var ffunction = new FFunction(sboxAddressesFactory, sboxFunction);

            return new Algorithm(ffunction, compressedKeyFactory);
        }
    }
}
