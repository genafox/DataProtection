using System.Collections;
using System.Linq;
using DES;
using DES.Domain;
using DES.Infrastructure;
using DES.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.DES.Utils;

namespace Tests.DES
{
    [TestClass]
    public class DesEncryptorTests
    {
        [TestMethod]
        public void Encrypt_WhenDataAndKeyAreValid_ReturnsEncryptedString()
        {
            string originalMessage = "hello world";
            string key = "13-34-57-79-9B-BC-DF-F1";

            Algorithm algorithm = FakeObjectsFactory.CreateAlgorithm();
            var encryptor = new DesEncryptor(algorithm);

            string encryptedMessage = encryptor.Encrypt(originalMessage, key);

            Assert.AreEqual("85BA5C74636F2CAC8E8ECF57F6A8AAB7", encryptedMessage.Split('+')[0]);
        }

        [TestMethod]
        public void Decrypt_WhenEncryptedStringIsValid_ReturnsDecryptedMessage()
        {
            string originalMessage = "hello world";
            string key = "13-34-57-79-9B-BC-DF-F1";

            Algorithm algorithm = FakeObjectsFactory.CreateAlgorithm();
            var encryptor = new DesEncryptor(algorithm);

            string encryptedMessage = encryptor.Encrypt(originalMessage, key);

            string decryptedMessage = encryptor.Decrypt(encryptedMessage, key);

            Assert.AreEqual(originalMessage, decryptedMessage);
        }
    }
}
