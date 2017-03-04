using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DES.Domain.SBox;
using DES.Misc;

namespace DES.Domain
{
    public class FFunction
    {
        private static readonly int[] ExtensionTable =
        {
            32, 1, 2, 3, 4, 5,
            4, 5, 6, 7, 8, 9,
            8, 9, 10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32, 1
        };

        private static readonly int[] PermutationTable =
        {
            58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,
            64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17,  9, 1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7
        };

        private readonly SBoxAddressFactory sboxAddressFactory;
        private readonly SBoxFunction sboxFunction;

        public FFunction(SBoxAddressFactory sboxAddressFactory, SBoxFunction sboxFunction)
        {
            this.sboxAddressFactory = sboxAddressFactory;
            this.sboxFunction = sboxFunction;
        }

        public BitArray Invoke(BitArray rightPart, BitArray key)
        {
            BitArray extendedRightPart = rightPart.Transform(ExtensionTable);

            BitArray multiplied = key.Xor(extendedRightPart);

            IDictionary<int, SBoxAddress> addresses = sboxAddressFactory.GetAddresses(multiplied);

            BitArray[] compressedParts = addresses
                .Select(pair => this.sboxFunction.Invoke(pair.Key, pair.Value))
                .ToArray();

            BitArray result = new BitArray(new bool[0])
                .Concat(compressedParts)
                .Transform(PermutationTable);

            return result;
        }
    }
}
