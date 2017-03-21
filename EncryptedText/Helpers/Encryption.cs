using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace EncryptedText.Helpers
{
    public class Encryption
    {
        static readonly string EntropyString = ConfigurationManager.AppSettings.Get("EntropyString");
        static readonly byte[] EntropyBytes = Convert.FromBase64String(EntropyString);

        public static string Encrypt(string toEncrypt, DataProtectionScope dataProtectionScope = DataProtectionScope.LocalMachine)
        {
            var cipherText = ProtectedData.Protect(Encoding.UTF8.GetBytes(toEncrypt), EntropyBytes, dataProtectionScope);
            return Convert.ToBase64String(cipherText);
        }

        public static string Decrypt(string toDecrypt, string entropyString, DataProtectionScope dataProtectionScope = DataProtectionScope.LocalMachine)
        {
            byte[] entropyBytes = Convert.FromBase64String(entropyString);

            var toDecryptBytes = Convert.FromBase64String(toDecrypt);
            var decrypted =
                Encoding.UTF8.GetString(ProtectedData.Unprotect(toDecryptBytes, entropyBytes, dataProtectionScope));
            return decrypted;
        }
    }
}
