using DES.Domain;
using DES.Domain.Key;
using DES.Domain.SBox;

namespace Tests.DES.Utils
{
    public static class FakeObjectsFactory
    {
        public static Algorithm CreateAlgorithm()
        {
            var compressedKeyFactory = new CompressedPermutedKeyFactory();

            var sboxFunction = new SBoxFunction();
            var sboxAddressesFactory = new SBoxAddressFactory();
            var ffunction = new FFunction(sboxAddressesFactory, sboxFunction);

            return new Algorithm(ffunction, compressedKeyFactory);
        }
    }
}
