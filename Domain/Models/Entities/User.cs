using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;

public sealed class User : BaseEntity
{
    [StringLength(60)]
    [Required(ErrorMessage = "Primeiro nome é obrigatório!", AllowEmptyStrings = false)]
    [Column("primeiro_nome")]
    public string FirstName { get; private set; }

    [StringLength(80)]
    [Required(ErrorMessage = "Segundo nome é obrigatório!", AllowEmptyStrings = false)]
    [Column("segundo_nome")]
    public string LastName { get; private set; }

    [StringLength(100)]
    [Required(ErrorMessage = "E-mail é obrigatório", AllowEmptyStrings = false)]
    [Column("email")]
    public string Email { get; private set; }

    [StringLength(20)]
    [Required(ErrorMessage = "Telefone é obrigatório", AllowEmptyStrings = false)]
    [Column("telefone")]
    public string Phone { get; private set; }

    [StringLength(200)]
    [Required(ErrorMessage = "Senha criptografada é obrigatório", AllowEmptyStrings = false)]
    [Column("senha_criptografada")]
    public string HashPassword { get; private set; }

    [StringLength(50)]
    [Required(ErrorMessage = "Role é obrigatório", AllowEmptyStrings = false)]
    [Column("role")]
    public string? Role { get; private set; } = "user";

    [StringLength(20)] [Column("codigo")] public string? CodeToResetPassword { get; private set; }

    public ICollection<Post> Posts { get; private set; }

    public void SetHasPassword(string password)
    {
        HashPassword = password;
    }
    public void SetCodeToResetPassword(string code)
    {
        CodeToResetPassword = code;
    }
    public void SetRole(string role)
    {
        Role = role;
    }
}