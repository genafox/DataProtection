using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Client
{
    public static class Extensions
    {
        // Creates string from BitArray (example 0111 0010 => "01110010")
        public static string ToBitString(this BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }

        public static BitArray GetRange(this BitArray bits, int startIndex, int length)
        {
            var result = new BitArray(length);

            for (int i = startIndex; i < startIndex + length; i++)
            {
                result[i] = bits[i];
            }

            return result;
        }

        public static BitArray CycleLeftShift(this BitArray bits, int shift)
        {
            var result = new BitArray(bits.Length);

            for (int i = bits.Length - 1, j = 0; i >= bits.Length - shift; i--, j++)
            {
                result[i] = bits[j];
            }

            for (int i = shift - 1, j = 0; i < bits.Length; i++, j++)
            {
                result[j] = bits[i];
            }

            return result;
        }

        public static BitArray Concat(this BitArray first, BitArray second)
        {
            bool[] combined = new bool[first.Length + second.Length];

            first.CopyTo(combined, 0);
            second.CopyTo(combined, first.Length);

            return new BitArray(combined);
        }

        // Magic
        public static byte[] ReverseBytes(this byte[] bytes)
        {
            return bytes.Select(b => (byte)((b * 0x0202020202 & 0x010884422010) % 1023)).ToArray();
        }

        public static string ToHexString(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        public static byte[] GetRange(this byte[] bytes, int startIndex, int lenght)
        {
            var result = new byte[lenght];

            for (int i = 0; i < lenght; i++)
            {
                result[i] = bytes[i + startIndex];
            }

            return result;
        }
    }
}
