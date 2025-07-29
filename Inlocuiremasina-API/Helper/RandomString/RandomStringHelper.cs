using System.Security.Cryptography;

namespace Helper.RandomString
{
    public class RandomStringHelper
    {
        public static string RandomTokenString(int digits)
        {
            byte[] randomBytes = new byte[digits];
            RandomNumberGenerator.Fill(randomBytes); // Use modern RandomNumberGenerator

            // Convert random bytes to a hexadecimal string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }
}
