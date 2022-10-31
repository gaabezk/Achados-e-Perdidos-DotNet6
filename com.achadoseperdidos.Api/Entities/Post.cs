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

    public Post(int id, string title, Item item)
    {
        DomainValidationException.When(id < 0, "Id nao pode ser menor ou igual a 0 (zero)!");
        Id = id;
        Validation(title, item);
    }

    public Post(string title, Item item)
    {
        Validation(title, item);
    }

    [Key] public int Id { get; private set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Titulo é obrigatório", AllowEmptyStrings = false)]
    [Column("titulo")]
    public string Title { get; private set; }

    public int? ItemId { get; private set; }
    public Item Item { get; private set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Data criacao é obrigatório", AllowEmptyStrings = false)]
    [Column("data_criacao")]
    public DateOnly? CreationDate { get; private set; }

    [Column("ultimo_update")] 
    public DateOnly? LastUpdateDate { get; private set; }

    [StringLength(30)]
    [Required(ErrorMessage = "Status da postagem é obrigatório", AllowEmptyStrings = false)]
    [Column("status_postagem")]
    public string? PostStatus { get; private set; }

    private void Validation(string title, Item item)
    {
        DomainValidationException.When(string.IsNullOrEmpty(title), "Nome deve ser informado!");

        Item = item;
        Title = title;
        CreationDate = DateOnly.FromDateTime(DateTime.Now);
        LastUpdateDate = DateOnly.FromDateTime(DateTime.Now);
        PostStatus = Enum.PostStatus.WAITING_APPROVAL.ToString();
    }

    public void setItem(Item item)
    {
        Item = item;
    }
    
    public override string ToString()
    {
        return
            $"ID: {Id}\nTITLE: {Title}\nITEM: {Item}\nCREATIONDATE: {CreationDate}\nLASTUPDATEDATE: {LastUpdateDate}\nPOSTSTATUS: {PostStatus}";
    }
}