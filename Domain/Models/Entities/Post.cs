using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;

public class Post : BaseEntity
{
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

    [Column("data_criacao")] 
    public DateOnly CreationDate { get; private set; }

    [Column("ultimo_update")] 
    public DateOnly LastUpdateDate { get; private set; }

    [StringLength(30)]
    [Required(ErrorMessage = "Status da postagem é obrigatório", AllowEmptyStrings = false)]
    [Column("status_postagem")]
    public string? PostStatus { get; private set; }

    public User User { get; private set; }
    public Guid UserId { get; private set; }
    
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