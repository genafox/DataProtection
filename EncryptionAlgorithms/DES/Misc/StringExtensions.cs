using System;
using System.Linq;

namespace DES.Misc
{
    public static class StringExtensions
    {
        public static byte[] GetBytesFromHex(this string hexString)
        {
            return hexString
                .Split('-')
                .Select(hex => Convert.ToByte(hex, 16))
                .ToArray();
        }

        public static byte[] GetBytesFromBinary(this string binaryString)
        {
            return binaryString
                .Split('-')
                .Select(binary => Convert.ToByte(binary, 2))
                .ToArray();
        }

        public static string GetHexFromBinary(this string binaryString)
        {
            string[] hexArray = binaryString
                .Split('-')
                .Select(binary => Convert.ToByte(binary, 2))
                .Select(b => Convert.ToString(b, 16))
                .ToArray();

            return string.Join("-" ,hexArray);
        }
    }
}
