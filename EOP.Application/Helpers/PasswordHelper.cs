using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace EOP.Application.Helpers
{
    public static class PasswordHelper
    {
        public static string Encrypt(string password)
        {
            // Generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        public static bool Verify(string hashedPasswordWithSalt, string passwordToVerify)
        {
            var parts = hashedPasswordWithSalt.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var hashed = Convert.FromBase64String(parts[1]);

            var calculatedHash = KeyDerivation.Pbkdf2(
                password: passwordToVerify,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return hashed.SequenceEqual(calculatedHash);
        }
    }
}
