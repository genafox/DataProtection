using System.Collections;
using System.Linq;
using Autofac;
using Common.Extensions;
using DES.Domain.Interfaces;
using DES.Domain.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoC;

namespace Tests.DES
{
    [TestClass]
    public class AlgorithmTests
    {
        private readonly IContainer resolver = ContainerInitializer.GetContainer();

        [TestMethod]
        public void Encrypt_WhenDataAndKeyAreValid_ReturnsEncryptedData()
        {
            string originalMessage = "hello world";
            var parsedToken = new ParsedToken(originalMessage);

            var keyBits = new BitArray("13-34-57-79-9B-BC-DF-F1".GetBytesFromHex());

            var algorithm = this.resolver.Resolve<IDesAlgorithm>();

            BitArray[] encryptedResult = parsedToken.BitBlocks.Select(block => algorithm.Encrypt(block, keyBits)).ToArray();
            BitArray[] decryptedResult = encryptedResult.Select(block => algorithm.Decrypt(block, keyBits)).ToArray();

            var decryptedToken = new DecryptedToken(parsedToken.OriginalBytesCount, decryptedResult);

            Assert.AreEqual(originalMessage, decryptedToken.ToString());
        }
    }
}
