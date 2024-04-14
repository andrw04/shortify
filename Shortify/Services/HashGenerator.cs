using System.Security.Cryptography;
using System.Text;

namespace Shortify.Services
{
    public static class HashGenerator
    {
        public static string Hash()
        {
            int length = 32;

            using (SHA1 sha1 = SHA1.Create())
            {
                return GetHash(sha1, GetRandomString(length));
            }
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();

            foreach (byte b in data)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        private static string GetRandomString(int length)
        {
            string chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            Random random = new Random();

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }

            return builder.ToString();
        }
    }
}
