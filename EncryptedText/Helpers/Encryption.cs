using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using static System.Text.Encoding;
using static System.Convert;

namespace EncryptedText.Helpers
{
    public class Encryption
    {
        static readonly string EntropyString = ConfigurationManager.AppSettings.Get("EntropyString");
        static readonly byte[] EntropyBytes = UTF8.GetBytes(EntropyString);

        public static string Encrypt(string toEncrypt, DataProtectionScope dataProtectionScope = DataProtectionScope.LocalMachine)
        {
            var cipherText = ProtectedData.Protect(UTF8.GetBytes(toEncrypt), EntropyBytes, dataProtectionScope);
            return ToBase64String(cipherText);
        }

        public static string Decrypt(string toDecrypt, string entropyString, DataProtectionScope dataProtectionScope = DataProtectionScope.LocalMachine)
        {
            byte[] entropyBytes = UTF8.GetBytes(entropyString);

            var toDecryptBytes = FromBase64String(toDecrypt);
            var decrypted =
                UTF8.GetString(ProtectedData.Unprotect(toDecryptBytes, entropyBytes, dataProtectionScope));
            return decrypted;
        }
    }
}
