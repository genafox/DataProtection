using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DES.Misc
{
    public static class BitArrayExtensions
    {
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

            for (int originalIdx = 0, shiftedIdx = shift; originalIdx < bits.Length; originalIdx++)
            {
                shiftedIdx = shiftedIdx == bits.Length ? 0 : shiftedIdx + 1;
                result[originalIdx] = bits[shiftedIdx];
            }

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

        public static BitArray Transform(this BitArray source, int[] transformationTable)
        {
            var resultArray = new BitArray(transformationTable.Length);

            for (int i = 0; i < transformationTable.Length; i++)
            {
                resultArray[i] = source[transformationTable[i]];
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