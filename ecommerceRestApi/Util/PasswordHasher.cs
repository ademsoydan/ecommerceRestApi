using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ecommerceRestApi.Util
{
	public static class PasswordHasher
	{
        public static string HashPassword(string password)
        {
            // A salt is required for password hashing
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            // Return the salt and the hashed password as a single string for storage
            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        public static bool VerifyHashedPassword(string hashedPasswordWithSalt, string passwordToCheck)
        {
            var parts = hashedPasswordWithSalt.Split('.');
            if (parts.Length != 2)
            {
                throw new FormatException("Invalid hashed password format");
            }

            byte[] salt = Convert.FromBase64String(parts[0]);
            string hashedPassword = parts[1];

            string hashedPasswordToCheck = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordToCheck,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashedPassword == hashedPasswordToCheck;
        }
    }
}

