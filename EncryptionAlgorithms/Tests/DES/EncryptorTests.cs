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


            var compressedKeyFactory = new CompressedPermutedKeyFactory();

            var sboxFunction = new SBoxFunction();
            var sboxAddressesFactory = new SBoxAddressFactory();
            var ffunction = new FFunction(sboxAddressesFactory, sboxFunction);

            var encryptor = new Encryptor(ffunction, compressedKeyFactory);
        }
    }
}
