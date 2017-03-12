using System.Collections;
using System.Security.Cryptography;

namespace Client
{
    internal class KeyService
    {
        private static readonly ArrayList WeakKeys = new ArrayList
        {
            new BitArray(new byte[] { 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01 }.ReverseBytes()),
            new BitArray(new byte[] { 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE }.ReverseBytes()),
            new BitArray(new byte[] { 0x1F, 0x1F, 0x1F, 0x1F, 0x0E, 0x0E, 0x0E, 0x0E }.ReverseBytes()),
            new BitArray(new byte[] { 0xE0, 0xE0, 0xE0, 0xE0, 0xF1, 0xF1, 0xF1, 0xF1 }.ReverseBytes())
        };

        public static BitArray GenerateKey()
        {
            BitArray keyBitArray;
            do
            {
                byte[] randomByteArray = GetRandomBytes(7);
                randomByteArray = randomByteArray.ReverseBytes();
                var randomBitArray = new BitArray(randomByteArray);

                keyBitArray = SetParityBits(randomBitArray);
            } while (WeakKeys.Contains(keyBitArray));

            return keyBitArray;
        }

        private static BitArray SetParityBits(BitArray keyBitArray)
        {
            var resultBitArray = new BitArray(64);
            int counter = 0;
            for (int i = 0, j = 0; i < keyBitArray.Length + 1; i++, j++)
            {
                if ((j + 1) % 8 == 0 && i != 0)
                {
                    resultBitArray[j] = counter % 2 == 0;
                    if (i == keyBitArray.Length)
                    {
                        break;
                    }
                    counter = keyBitArray[i] ? 1 : 0;
                    j++;
                    resultBitArray[j] = keyBitArray[i];
                }
                else if (keyBitArray[i])
                {
                    resultBitArray[j] = true;
                    counter++;
                }
                else
                {
                    resultBitArray[j] = false;
                }
            }

            return resultBitArray;
        }

        private static byte[] GetRandomBytes(int length)
        {
            byte[] result = new byte[length];
            RandomNumberGenerator randomNumberGenerator = new RNGCryptoServiceProvider();

            randomNumberGenerator.GetBytes(result);

            return result;
        }
    }
}