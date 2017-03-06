namespace Contracts.Interfaces
{
    public interface IDataEncryptor
    {
        string Encrypt(string message, string key);

        string Decrypt(string encryptedMessage, string key);
    }
}
