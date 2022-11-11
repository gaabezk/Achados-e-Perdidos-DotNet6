using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.achadoseperdidos.Api.Validations;

namespace com.achadoseperdidos.Api.Entities;

[Table("tb_post")]
public class Post
{
    public Post(string itemName, string description, string? imageUrl1, string? imageUrl2, string? imageUrl3,
        string? color, string foundLocation, DateOnly? itemDateFound, string city, DateOnly creationDate,
        DateOnly lastUpdateDate, string postStatus, int userId)
    {
        Validation(itemName, description, imageUrl1, imageUrl2, imageUrl3, color, foundLocation, itemDateFound, city,
            creationDate, lastUpdateDate, postStatus, userId);
    }

    public Post(int id, string itemName, string description, string? imageUrl1, string? imageUrl2, string? imageUrl3,
        string? color, string foundLocation, DateOnly? itemDateFound, string city, DateOnly creationDate,
        DateOnly lastUpdateDate, string postStatus, int userId)
    {
        DomainValidationException.When(id < 0, "Id nao pode ser menor que 0 (zero)!");
        Id = id;
        Validation(itemName, description, imageUrl1, imageUrl2, imageUrl3, color, foundLocation, itemDateFound, city,
            creationDate, lastUpdateDate, postStatus, userId);
    }

    public Post()
    {
    }

    [Key] public int Id { get; private set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Nome do item perdido é obrigatório!", AllowEmptyStrings = false)]
    [Column("item_name")]
    public string ItemName { get; private set; }

    [StringLength(600)]
    [Required(ErrorMessage = "Descrição é obrigatoria!", AllowEmptyStrings = false)]
    [Column("descricao")]
    public string Description { get; private set; }

    [StringLength(500)]
    [Column("imagem_url1")]
    public string? ImageUrl1 { get; private set; }

    [StringLength(500)]
    [Column("imagem_url2")]
    public string? ImageUrl2 { get; private set; }

    [StringLength(500)]
    [Column("imagem_url3")]
    public string? ImageUrl3 { get; private set; }

    [StringLength(50)]
    [Column("item_cor")]
    public string? Color { get; private set; }

    [StringLength(600)]
    [Required(ErrorMessage = "Local que o item foi encontrado é obrigatorio!", AllowEmptyStrings = false)]
    [Column("local_encontrado")]
    public string FoundLocation { get; private set; }

    [Column("data_encontrado")] public DateOnly? ItemDateFound { get; private set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Cidade é obrigatoria", AllowEmptyStrings = false)]
    [Column("cidade")]
    public string City { get; private set; }

    [Column("data_criacao")] public DateOnly CreationDate { get; private set; }

    [Column("ultimo_update")] public DateOnly LastUpdateDate { get; private set; }

    [StringLength(30)]
    [Required(ErrorMessage = "Status da postagem é obrigatório", AllowEmptyStrings = false)]
    [Column("status_postagem")]
    public string PostStatus { get; private set; }

    public User User { get; set; }
    public int UserId { get; set; }

    private void Validation(string itemName, string description, string? imageUrl1, string? imageUrl2,
        string? imageUrl3, string? color, string foundLocation, DateOnly? itemDateFound, string city,
        DateOnly creationDate, DateOnly lastUpdateDate, string postStatus, int userId)
    {
        DomainValidationException.When(string.IsNullOrEmpty(itemName), "Nome do item deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(description), "Descrição deve ser informada!");
        DomainValidationException.When(string.IsNullOrEmpty(foundLocation), "Lugar encontrado deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(city), "Cidade deve ser informada!");
        DomainValidationException.When(string.IsNullOrEmpty(postStatus), "Status do post deve ser informado!");
        DomainValidationException.When(userId <= 0, "Id do usuario nao pode ser menor ou igual a 0 (zero)!");
        
        UserId = userId;
        ItemName = itemName;
        Description = description;
        ImageUrl1 = imageUrl1;
        ImageUrl2 = imageUrl2;
        ImageUrl3 = imageUrl3;
        Color = color;
        FoundLocation = foundLocation;
        ItemDateFound = itemDateFound;
        City = city;
        CreationDate = creationDate;
        LastUpdateDate = lastUpdateDate;
        PostStatus = postStatus;
    }

    public void SetPostStatus(string status)
    {
        PostStatus = status;
    }

    public void SetLastUpdateDate(DateOnly date)
    {
        LastUpdateDate = date;
    }

    public void SetCreationDate(DateOnly date)
    {
        CreationDate = date;
    }
}