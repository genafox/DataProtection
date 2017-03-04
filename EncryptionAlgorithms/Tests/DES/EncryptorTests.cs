using System;
using System.Collections;
using System.Linq;
using DES.Domain;
using DES.Domain.Key;
using DES.Domain.SBox;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DES
{
    [TestClass]
    public class EncryptorTests
    {
        [TestMethod]
        public void Encrypt_WhenDataAndKeyAreValid_ReturnsEncryptedData()
        {
            byte[] messageBytes = HexStringToBytes("01-23-45-67-89-AB-CD-EF");
            byte[] keyBytes = HexStringToBytes("13-34-57-79-9B-BC-DF-F1");

            byte[] expectedMessageBytes = HexStringToBytes("85-E8-13-54-0F-0A-B4-05");

            var compressedKeyFactory = new CompressedPermutedKeyFactory();

            var sboxFunction = new SBoxFunction();
            var sboxAddressesFactory = new SBoxAddressFactory();
            var ffunction = new FFunction(sboxAddressesFactory, sboxFunction);

            var encryptor = new Encryptor(ffunction, compressedKeyFactory);

            BitArray result = encryptor.Encrypt(new BitArray(messageBytes), new BitArray(keyBytes));

            CollectionAssert.AreEqual(expectedMessageBytes, result);
        }

        private static byte[] HexStringToBytes(string hexString)
        {
            return hexString
                .Split('-')
                .Select(hex => Convert.ToByte(hex, 16))
                .ToArray();
        }
    }
}
