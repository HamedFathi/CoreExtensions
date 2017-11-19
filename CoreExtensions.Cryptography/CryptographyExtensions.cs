using Force.Crc32;
using Rijndael256;
using System.Text;

namespace CoreExtensions
{
    public static class CryptographyExtensions
    {
        public static string DecryptByRijndael(this string plaintext, string password, KeySize keySize = KeySize.Aes128)
        {
            return Rijndael.Decrypt(plaintext, password, keySize);
        }

        public static string EncryptByRijndael(this string plaintext, string password, KeySize keySize = KeySize.Aes128)
        {
            return Rijndael.Encrypt(plaintext, password, keySize);
        }

        public static string HashByBCrypt(this string plaintext)
        {
            return BCrypt.Net.BCrypt.HashPassword(plaintext);
        }

        public static int HashByCrc32(this string text)
        {
            return (int)Crc32Algorithm.Compute(Encoding.Unicode.GetBytes(text));
        }

        public static bool VerifyHashByBCrypt(this string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }
    }
}
