using System;
using System.Collections;
using System.Dynamic;
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
            var parser = new Parser();
            ParsedToken token = parser.Parse(originalMessage);

            var keyBits = new BitArray("13-34-57-79-9B-BC-DF-F1".GetBytesFromHex());

            Encryptor encryptor = CreateEncryptor();

            BitArray[] encryptedResult = token.ExtractedBits.Select(block => encryptor.Encrypt(block, keyBits)).ToArray();
            BitArray[] decryptedResult = encryptedResult.Select(block => encryptor.Decrypt(block, keyBits)).ToArray();

            string decryptedMessage = parser.GetString(new ParsedToken(token.OriginalBytesCount, decryptedResult));

            Assert.AreEqual(originalMessage, decryptedMessage);
        }

        private static Encryptor CreateEncryptor()
        {
            var compressedKeyFactory = new CompressedPermutedKeyFactory();

            var sboxFunction = new SBoxFunction();
            var sboxAddressesFactory = new SBoxAddressFactory();
            var ffunction = new FFunction(sboxAddressesFactory, sboxFunction);

            return new Encryptor(ffunction, compressedKeyFactory);
        }
    }
}
