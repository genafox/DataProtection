using System;
using Contracts.Interfaces;

namespace Common.LogDecorators
{
    public class DataEncryptorLogDecorator : IDataEncryptor
    {
	    private const string TabLevel = "";

        private readonly IDataEncryptor encryptor;
        private readonly ILogger logger;

        public DataEncryptorLogDecorator(IDataEncryptor encryptor, ILogger logger)
        {
            this.encryptor = encryptor;
            this.logger = logger;
        }

        public string Encrypt(string message, string key)
        {
            Type decoratedType = this.encryptor.GetType();
            this.logger.LogInfo(TabLevel + $"{decoratedType.FullName}.Encrypt - Start - Original message: {message} - Key: {key}");

            string result = this.encryptor.Encrypt(message, key);

            this.logger.LogInfo(TabLevel + $"{decoratedType.FullName}.Encrypt - End - Encrypted message: {result}");

            return result;
        }

        public string Decrypt(string encryptedMessage, string key)
        {
            Type decoratedType = this.encryptor.GetType();
            this.logger.LogInfo(TabLevel + $"{decoratedType.FullName}.Decrypt - Start - Encrypted message: {encryptedMessage} - Key: {key}");

            string result = this.encryptor.Decrypt(encryptedMessage, key);

            this.logger.LogInfo(TabLevel + $"{decoratedType.FullName}.Decrypt - End - Decrypted message: {result}");

            return result;
        }
    }
}
