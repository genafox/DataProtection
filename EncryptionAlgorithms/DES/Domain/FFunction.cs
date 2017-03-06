using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DES.Domain.SBox;
using Common.Extensions;

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
            16, 7, 20, 21,
            29, 12, 28, 17,
            1, 15, 23, 26,
            5, 18, 31, 10,
            2, 8, 24, 14,
            32, 27, 3, 9,
            19, 13, 30, 6,
            22, 11, 4, 25
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
            BitArray extendedRightPart = rightPart.Transform(ExtensionTable, tableStartIndex: 1);

            BitArray multiplied = key.Xor(extendedRightPart);

            IDictionary<int, SBoxAddress> addresses = sboxAddressFactory.GetAddresses(multiplied);

            BitArray[] compressedParts = addresses
                .Select(pair => this.sboxFunction.Invoke(pair.Key, pair.Value))
                .ToArray();

            BitArray result = new BitArray(new bool[0])
                .Concat(compressedParts)
                .Transform(PermutationTable, tableStartIndex: 1);

            return result;
        }
    }
}
