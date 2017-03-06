using Autofac;
using Contracts.Interfaces;
using IoC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DES
{
    [TestClass]
    public class DesEncryptorTests
    {
        private readonly IContainer resolver = ContainerInitializer.GetContainer();

        [TestMethod]
        public void Encrypt_WhenDataAndKeyAreValid_ReturnsEncryptedString()
        {
            string originalMessage = "hello world";
            string key = "13-34-57-79-9B-BC-DF-F1";

            var encryptor = this.resolver.Resolve<IDataEncryptor>();

            string encryptedMessage = encryptor.Encrypt(originalMessage, key);

            Assert.AreEqual("85BA5C74636F2CAC8E8ECF57F6A8AAB7", encryptedMessage.Split('+')[0]);
        }

        [TestMethod]
        public void Decrypt_WhenEncryptedStringIsValid_ReturnsDecryptedMessage()
        {
            string originalMessage = "hello world";
            string key = "13-34-57-79-9B-BC-DF-F1";

            var encryptor = this.resolver.Resolve<IDataEncryptor>();

            string encryptedMessage = encryptor.Encrypt(originalMessage, key);

            string decryptedMessage = encryptor.Decrypt(encryptedMessage, key);

            Assert.AreEqual(originalMessage, decryptedMessage);
        }
    }
}
