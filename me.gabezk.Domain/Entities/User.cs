using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using me.gabezk.Domain.Validations;

namespace me.gabezk.Domain.Entities;

[Table("tb_usuario")]
public sealed class User
{
    public User(string fullName, string email, string phone, string password)
    {
        Validation(fullName, email, phone, password);
    }

    public User(int id, string fullName, string email, string phone, string password)
    {
        DomainValidationException.When(id < 0, "Id nao pode ser menor que 0 (zero)!");
        Id = id;
        Validation(fullName, email, phone, password);
    }

    [Key] public int Id { get; private set; }

    [StringLength(80)]
    [Required(ErrorMessage = "Nome completo é obrigatório", AllowEmptyStrings = false)]
    [Column("nome_completo")]
    public string FullName { get; private set; }

    [StringLength(100)]
    [Required(ErrorMessage = "E-mail é obrigatório", AllowEmptyStrings = false)]
    [Column("email")]
    public string Email { get; private set; }

    [StringLength(14)]
    [Required(ErrorMessage = "Telefone é obrigatório", AllowEmptyStrings = false)]
    [Column("telefone")]
    public string Phone { get; private set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Senha é obrigatório", AllowEmptyStrings = false)]
    [Column("senha")]
    public string Password { get; private set; }

    [StringLength(20)]
    [Required(ErrorMessage = "Role é obrigatório", AllowEmptyStrings = false)]
    [Column("role")]
    public string Role { get; private set; }

    [StringLength(100)] [Column("codigo")] public string? CodeToResetPassword { get; private set; }

    public ICollection<Post> Posts { get; set; }

    private void Validation(string fullName, string email, string phone, string password)
    {
        DomainValidationException.When(string.IsNullOrEmpty(fullName), "Nome completo deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(email), "E-mail deve ser informadooooooooooooo!");
        DomainValidationException.When(string.IsNullOrEmpty(phone), "Telefone deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(password), "Senha deve ser informada!");

        FullName = fullName;
        Email = email;
        Phone = phone;
        Password = password;
        Role = Enum.Role.USER.ToString();
        Posts = new List<Post>();
    }

    public void setRole(string role)
    {
        Role = role;
    }
}