namespace AdventureAPI.Core.Interfaces;

public interface IPasswordService
{
    string Hash(string password, out byte[] salt);
    bool VerifyPassword(string password, string storedHash, byte[] storedSalt);
}
