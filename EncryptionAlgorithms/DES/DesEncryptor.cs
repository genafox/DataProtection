using System.Collections;
using System.Linq;
using DES.Domain;
using DES.Infrastructure;
using DES.Misc;

namespace DES
{
    public class DesEncryptor
    {
        private readonly Algorithm algorithm;
        private readonly Parser parser;

        public DesEncryptor(Algorithm algorithm, Parser parser)
        {
            this.algorithm = algorithm;
            this.parser = parser;
        }

        public string Encrypt(string message, string hexKey)
        {
            ParsedToken parsedToken = this.parser.ParseText(message);
            var keyBits = new BitArray(hexKey.GetBytesFromHex());

            BitArray[] encryptedBits = parsedToken.BitBlocks
                .Select(bitsBlock => algorithm.Encrypt(bitsBlock, keyBits))
                .ToArray();
            
            var encryptedToken = new EncryptedToken(parsedToken.OriginalBytesCount, encryptedBits);
            return encryptedToken.ToString();
        }

        public string Decrypt(string encryptedMessage, string hexKey)
        {
            ParsedToken token = this.parser.Parse(encryptedMessage);
            var keyBits = new BitArray(hexKey.GetBytesFromHex());

            BitArray[] dencryptedResult = token.ExtractedBits
                .Select(bitsBlock => algorithm.Decrypt(bitsBlock, keyBits))
                .ToArray();

            string decryptedMessage = parser.GetString(new ParsedToken(token.OriginalBytesCount, decryptedResult));
        }
    }
}
