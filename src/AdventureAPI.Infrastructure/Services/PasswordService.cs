using System.Security.Cryptography;
using System.Text;
using AdventureAPI.Core.Interfaces;

namespace AdventureAPI.Infrastructure.Services;

public class PasswordService : IPasswordService
{
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    public string Hash(string password, out byte[] salt)
    {
        // Generate a salt
        salt = RandomNumberGenerator.GetBytes(KeySize);

        // Hash the password with PBKDF2
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            _hashAlgorithm,
            KeySize);
        return Convert.ToHexString(hash);
    }

    public bool VerifyPassword(string password, string storedHash, byte[] storedSalt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            password,
            storedSalt,
            Iterations,
            _hashAlgorithm,
            KeySize);
        return CryptographicOperations.FixedTimeEquals(
            hashToCompare,
            Convert.FromHexString(storedHash));
    }
}
