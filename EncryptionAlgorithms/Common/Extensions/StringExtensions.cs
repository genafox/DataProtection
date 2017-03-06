using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetBytesFromHex(this string hexString)
        {
            string[] fragments = GetFragments(hexString, 2, '-').ToArray();

            return fragments
                .Select(hex => Convert.ToByte(hex, 16))
                .ToArray();
        }

        public static byte[] GetBytesFromBinary(this string binaryString)
        {
            string[] fragments = GetFragments(binaryString, 8, '-').ToArray();

            return fragments
                .Select(binary => Convert.ToByte(binary, 2))
                .ToArray();
        }

        public static string GetHexFromBinary(this string binaryString)
        {
            string[] fragments = GetFragments(binaryString, 8, '-').ToArray();
            
            string[] hexArray = fragments
                .Select(binary => Convert.ToByte(binary, 2))
                .Select(b => Convert.ToString(b, 16))
                .ToArray();

            return string.Join("-" ,hexArray);
        }

        private static IEnumerable<string> GetFragments(string str, int fragmentLength, char? fragmentSeparator)
        {
            IEnumerable<string> fragments = fragmentSeparator.HasValue && str.Contains(fragmentSeparator.Value)
                ? str.Split(fragmentSeparator.Value)
                : Enumerable.Range(0, str.Length)
                    .Where(x => x % fragmentLength == 0)
                    .Select(x => str.Substring(x, fragmentLength));

            return fragments;
        }
    }
}
