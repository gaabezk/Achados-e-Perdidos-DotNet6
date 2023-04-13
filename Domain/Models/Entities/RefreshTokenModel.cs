namespace Domain.Models.Entities;

public class RefreshTokenModel : BaseEntity
{
    public RefreshTokenModel(string email, string refreshToken)
    {
        Email = email;
        RefreshToken = refreshToken;
    }

    public string Email { get; set; }
    public string RefreshToken { get; set; }
}