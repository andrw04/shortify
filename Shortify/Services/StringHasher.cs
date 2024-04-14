using System.Security.Cryptography;
using System.Text;

namespace Shortify.Services
{
    public static class StringHasher
    {
        public static string HashString(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                return GetHash(sha1, input);
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
    }
}
