using System.Text.Json.Serialization;

namespace Application.Dto;

public class UserEditRequestDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    [JsonIgnore]
    public string? Password { get; set; }
}