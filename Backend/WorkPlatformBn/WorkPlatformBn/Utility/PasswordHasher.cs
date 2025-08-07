using System.Security.Cryptography;

namespace WorkPlatformBn.Utility;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        // Generate a salt (a random string to make each hash unique)
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[16];  // 16 bytes of salt
            rng.GetBytes(salt);

            // Hash the password with the salt using PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);  // 32-byte hash

                // Combine salt and hash into one byte array for storage
                byte[] hashBytes = new byte[48];  // 16 bytes of salt + 32 bytes of hash
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 32);

                // Return the hash (salt + hash) as a Base64 string
                return Convert.ToBase64String(hashBytes);
            }
        }
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        // Convert the stored hash from Base64 to bytes
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        // Extract the salt (first 16 bytes)
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        // Extract the hash (remaining 32 bytes)
        byte[] storedPasswordHash = new byte[32];
        Array.Copy(hashBytes, 16, storedPasswordHash, 0, 32);

        // Hash the input password with the extracted salt
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
        {
            byte[] computedHash = pbkdf2.GetBytes(32);  // 32-byte hash

            // Compare the computed hash with the stored hash
            for (int i = 0; i < 32; i++)
            {
                if (computedHash[i] != storedPasswordHash[i])
                {
                    return false;  // Password doesn't match
                }
            }
        }

        // If we get here, the password is valid
        return true;
    }
}
