using com.achadoseperdidos.Api.Validations;

namespace com.achadoseperdidos.Api.Entities;

public sealed class User
{
    public int Id { get; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Password { get; private set; }
    public string Role { get; set; }
    public string? CodeToResetPassword { get; private set; }
    

    public User(string fullName, string email, string phone, string password)
    {
        Role = Enum.Role.USER.ToString();
        Validation(fullName,  email,  phone,  password);
    }

    public User(int id, string fullName, string email, string phone, string password)
    {
        DomainValidationException.When(id < 0,"Id nao pode ser menor ou igual a 0 (zero)!");
        Id = id;
        Validation(fullName,  email,  phone,  password);
    }

    private void Validation(string fullName, string email, string phone, string password)
    {
        DomainValidationException.When(string.IsNullOrEmpty(fullName),"Nome completo deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(email),"E-mail deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(phone),"Telefone deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(password),"Senha deve ser informada!");
        
        FullName = fullName;
        Email = email;
        Phone = phone;
        Password = password;
    }
    
}