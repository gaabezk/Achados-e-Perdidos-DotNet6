using System.Text.Json.Serialization;

namespace com.achadoseperdidos.Api.DTO;

public class PostDto
{
    public int Id { get; set; }
    public string? ItemName { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl1 { get; set; }
    public string? ImageUrl2 { get; set; }
    public string? ImageUrl3 { get; set; }
    public string? Color { get; set; }
    public string? FoundLocation { get; set; }
    public string? City { get; set; }

    [JsonIgnore] public DateOnly? CreationDate { get; set; }

    [JsonIgnore] public DateOnly? LastUpdateDate { get; set; }

    [JsonIgnore] public string? PostStatus { get; set; }

    /// <example>dd/MM/aaaa</example>
    public DateOnly? ItemDateFound { get; set; }
}