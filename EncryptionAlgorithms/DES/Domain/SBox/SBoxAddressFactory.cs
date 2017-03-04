using System.Collections;
using System.Collections.Generic;
using DES.Misc;

namespace DES.Domain.SBox
{
    public class SBoxAddressFactory
    {
        private const int AddressLength = 6;

        public IDictionary<int, SBoxAddress> GetAddresses(BitArray source)
        {
            var result = new Dictionary<int, SBoxAddress>();

            int addressesCount = source.Length / AddressLength;
            for (int i = 1; i <= addressesCount; i++)
            {
                int startIndex = AddressLength * (i - 1);
                BitArray address = source.GetRange(startIndex, AddressLength);

                result.Add(i, new SBoxAddress(address));
            }

            return result;
        }
    }
}
