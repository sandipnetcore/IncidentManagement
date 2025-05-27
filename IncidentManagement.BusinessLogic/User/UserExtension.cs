using System.Security.Cryptography;
using System.Text;

namespace IncidentManagement.BusinessLogic.User
{
    internal static class UserExtension
    {
        const int _keyLength = 64;
        const int _NumberOfIterations = 350000;
        static HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
        static byte[] _passwordHashedPrefix = RandomNumberGenerator.GetBytes(_keyLength);
        public static string HashPasword(this string password)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                _passwordHashedPrefix,
                _NumberOfIterations,
                _hashAlgorithm,
                _keyLength);

            return Convert.ToHexString(hash);
        }

    }
}
