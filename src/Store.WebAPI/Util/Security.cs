using System.Security.Cryptography;
using System.Text;

namespace Store.WebAPI.Util;

public static class Security
{
    private static readonly SHA256 Cipher = SHA256.Create();

    public static byte[] ComputeHash(string textToHash, int saltSize, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(saltSize);
        byte[] saltedTextBytes = Encoding.UTF8.GetBytes(textToHash).Concat(salt);
        return Cipher.ComputeHash(saltedTextBytes);
    }

    public static bool CompareHashes(string inputText, byte[] storedHash, byte[] salt)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(inputText).Concat(salt);
        byte[] inputHash = Cipher.ComputeHash(buffer);

        if (inputHash.Length != storedHash.Length) return false;

        for (var index = 0; index < inputHash.Length; ++index)
            if (inputHash[index] != storedHash[index])
                return false;

        return true;
    }
}
