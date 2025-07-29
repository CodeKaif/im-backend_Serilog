using System.Security.Cryptography;
using System.Text;

namespace Helper.PasswordHasher
{
    public class PasswordHasherHelper
    {
        private const string EncryptionKey = "A#$Derclfkdws"; // Security Key
        private static readonly byte[] Salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }; // Salt value

        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);

            using (Aes aes = Aes.Create())
            {
                using var pdb = new Rfc2898DeriveBytes(EncryptionKey, Salt, 100000, HashAlgorithmName.SHA256);
                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);

                using MemoryStream ms = new();
                using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string Decrypt(string cipherText)
        {
            try
            {
                cipherText = cipherText.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    using var pdb = new Rfc2898DeriveBytes(EncryptionKey, Salt, 100000, HashAlgorithmName.SHA256);
                    aes.Key = pdb.GetBytes(32);
                    aes.IV = pdb.GetBytes(16);

                    using MemoryStream ms = new();
                    using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.FlushFinalBlock();

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                return string.Empty; // Returning empty string instead of unhandled exception
            }
        }
    }
}
