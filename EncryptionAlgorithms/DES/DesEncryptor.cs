using System.Collections;
using System.Linq;
using Common.Extensions;
using Contracts.Interfaces;
using DES.Domain;
using DES.Infrastructure;

namespace DES
{
    public class DesEncryptor : IDataEncryptor
    {
        private readonly Algorithm algorithm;

        public DesEncryptor(Algorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        public string Encrypt(string message, string hexKey)
        {
            var parsedToken = new ParsedToken(message);
            var keyBits = new BitArray(hexKey.GetBytesFromHex());

            BitArray[] encryptedBits = parsedToken.BitBlocks
                .Select(bitsBlock => algorithm.Encrypt(bitsBlock, keyBits))
                .ToArray();
            
            var encryptedToken = new EncryptedToken(parsedToken.OriginalBytesCount, encryptedBits);

            return encryptedToken.ToString();
        }

        public string Decrypt(string encryptedMessage, string hexKey)
        {
            var encryptedToken = new EncryptedToken(encryptedMessage);
            var keyBits = new BitArray(hexKey.GetBytesFromHex());

            BitArray[] dencryptedResult = encryptedToken.EncryptedBitBlocks
                .Select(bitsBlock => algorithm.Decrypt(bitsBlock, keyBits))
                .ToArray();

            var decryptedToken = new DecryptedToken(encryptedToken.OriginalBytesCount, dencryptedResult);

            return decryptedToken.ToString();
        }
    }
}
