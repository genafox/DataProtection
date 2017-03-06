using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class BitArrayExtensions
    {
        public static BitArray GetRange(this BitArray bits, int startIndex, int length)
        {
            var result = new BitArray(length);

            for (int i = 0, j = startIndex; length > 0; i++, j++, length--)
            {
                result[i] = bits[j];
            }

            return result;
        }

        public static BitArray CycleLeftShift(this BitArray bits, int shift)
        {
            BitArray shiftedPart = bits.GetRange(0, shift);
            BitArray mainPart = bits.GetRange(shift, bits.Length - shift);

            BitArray result = mainPart.Concat(shiftedPart);

            return result;
        }

        public static BitArray Concat(this BitArray first, params BitArray[] arrays)
        {
            bool[] combined = new bool[first.Length + arrays.Select(array => array.Length).Sum()];

            first.CopyTo(combined, 0);
            int startIndex = first.Length;
            foreach (BitArray array in arrays)
            {
                array.CopyTo(combined, startIndex);
                startIndex += array.Length;
            }

            return new BitArray(combined);
        }

        public static BitArray Transform(this BitArray source, int[] transformationTable, int tableStartIndex = 0)
        {
            var resultArray = new BitArray(transformationTable.Length);

            for (int i = 0; i < transformationTable.Length; i++)
            {
                resultArray[i] = source[transformationTable[i] - tableStartIndex];
            }

            return resultArray;
        }

        public static IEnumerable<TResult> Select<TResult>(this BitArray source, Func<bool, TResult> getElementFunc)
        {
            foreach (bool bit in source)
            {
                yield return getElementFunc(bit);
            }
        }
    }
}