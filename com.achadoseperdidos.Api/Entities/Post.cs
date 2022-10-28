using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.achadoseperdidos.Api.Validations;

namespace com.achadoseperdidos.Api.Entities;

[Table("tb_post")]
public class Post
{
    public Post()
    {
    }

    public Post(string title, Item item, DateTime creationDate, string postStatus)
    {
        CreationDate = DateTime.Now;
        LastUpdateDate = DateTime.Now;
        Validation(title, item, creationDate, postStatus);
        PostStatus = Enum.PostStatus.WAITING_APPROVAL.ToString();
    }

    public Post(int id, string title, Item item, DateTime creationDate, DateTime lastUpdateDate, string postStatus)
    {
        DomainValidationException.When(id < 0, "Id nao pode ser menor ou igual a 0 (zero)!");
        Id = id;
        LastUpdateDate = DateTime.Now;
        Validation(title, item, creationDate, postStatus);
    }

    [Key]
    public int Id { get; private set; }
    
    [StringLength(100)]
    [Required(ErrorMessage="Titulo é obrigatório",AllowEmptyStrings=false)]
    [Column("titulo")]
    public string Title { get; private set; }
    public Item Item { get; private set; }
    
    [StringLength(100)]
    [Required(ErrorMessage="Data criacao é obrigatório",AllowEmptyStrings=false)]
    [Column("data_criacao")]
    public DateTime? CreationDate { get; }
    
    [Column("ultimo_update")]
    public DateTime? LastUpdateDate { get; }
    
    [StringLength(30)]
    [Required(ErrorMessage="Status da postagem é obrigatório",AllowEmptyStrings=false)]
    [Column("status_postagem")]
    public string? PostStatus { get; private set; }

    private void Validation(string title, Item item, DateTime creationDate, string postStatus)
    {
        DomainValidationException.When(string.IsNullOrEmpty(title), "Nome deve ser informado!");

        Title = title;
        Item = item;
        PostStatus = postStatus;
    }

    public override string ToString()
    {
        return
            $"ID: {Id}\nTITLE: {Title}\nITEM: {Item}\nCREATIONDATE: {CreationDate}\nLASTUPDATEDATE: {LastUpdateDate}\nPOSTSTATUS: {PostStatus}";
    }
}