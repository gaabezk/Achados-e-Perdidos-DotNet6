using me.gabezk.Domain.Entities;

namespace me.gabezk.Application.Dto;

public class PostDtoReturn
{
    public PostDtoReturn()
    {
    }

    public PostDtoReturn(Post model)
    {
        User = model.User.FullName;
        UserId = model.Id;
        ItemName = model.ItemName;
        Description = model.Description;
        ImageUrl1 = model.ImageUrl1;
        ImageUrl2 = model.ImageUrl2;
        ImageUrl3 = model.ImageUrl3;
        Color = model.Color;
        FoundLocation = model.FoundLocation;
        City = model.City;
        CreationDate = model.CreationDate;
        LastUpdateDate = model.LastUpdateDate;
        PostStatus = model.PostStatus;
        ItemDateFound = model.ItemDateFound;
    }

    public int? Id { get; set; }
    public string User { get; set; }
    public int? UserId { get; set; }
    public string? ItemName { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl1 { get; set; }
    public string? ImageUrl2 { get; set; }
    public string? ImageUrl3 { get; set; }
    public string? Color { get; set; }
    public string? FoundLocation { get; set; }
    public string? City { get; set; }
    public DateOnly? CreationDate { get; set; }
    public DateOnly? LastUpdateDate { get; set; }
    public string? PostStatus { get; set; }
    public DateOnly? ItemDateFound { get; set; }
}