using System.Text.Json.Serialization;

namespace Application.Dto;

public class PostResponseDto
{
    public Guid Id { get; set; }
    public string? ItemName { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl1 { get; set; }
    public string? ImageUrl2 { get; set; }
    public string? ImageUrl3 { get; set; }
    public string? Color { get; set; }
    public string? FoundLocation { get; set; }
    public string? City { get; set; }
    public DateOnly? ItemDateFound { get; set; }
    public DateOnly? CreationDate { get; set; }
    public DateOnly? LastUpdateDate { get; set; }
    public string? PostStatus { get; set; }
    public string? UserFullName { get; set; }
    public string? UserPhone { get; set; }
    
}