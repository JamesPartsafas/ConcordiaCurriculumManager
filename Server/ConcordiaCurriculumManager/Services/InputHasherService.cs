using System.Security.Cryptography;

namespace ConcordiaCurriculumManager.Services;

public interface IInputHasherService
{
    string Hash(string input);
    bool Verify(string input, string hashString);
}

public class InputHasherService : IInputHasherService
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 1_000_000;
    private const char segmentDelimiter = ':';
    private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

    public string Hash(string input)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            Iterations,
            _algorithm,
            KeySize
        );

        return string.Join(
            segmentDelimiter,
            Convert.ToHexString(hash),
            Convert.ToHexString(salt)
        );
    }

    public bool Verify(string input, string hashString)
    {
        string[] segments = hashString.Split(segmentDelimiter);
        byte[] hash = Convert.FromHexString(segments[0]);
        byte[] salt = Convert.FromHexString(segments[1]);
        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            Iterations,
            _algorithm,
            hash.Length
        );
        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }
}