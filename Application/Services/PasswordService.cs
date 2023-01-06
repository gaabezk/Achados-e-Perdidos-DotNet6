namespace Application.Services;

public abstract class PasswordService
{
    public static string GenerateRandomCharacters(int size)
    {
        return new string(Enumerable.Repeat("abcdefghijk0123456789@#lmnopqrstuvwxyz", size)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
    }
    
    public static string GenerateHashPassword(string value)
    {
        return BCrypt.Net.BCrypt.HashPassword(value);
    }    
    
    public static bool VerifyHashPassword(string pass, string hashPass)
    {
        return BCrypt.Net.BCrypt.Verify(pass, hashPass);
    }
}