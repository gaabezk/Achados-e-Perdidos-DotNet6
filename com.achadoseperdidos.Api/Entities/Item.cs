using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.achadoseperdidos.Api.Validations;

namespace com.achadoseperdidos.Api.Entities;
[Table("tb_item")]
public sealed class Item
{
    public Item(int id, string name, string description, string? imageUrl, string? color, string foundLocation,
        string? foundReference, string city, DateOnly dateFound)
    {
        DomainValidationException.When(id < 0, "Id nao pode ser menor ou igual a 0 (zero)!");
        Id = id;
        Validation(name, description, imageUrl, color, foundLocation, foundReference, city, dateFound);
    }

    public Item(string name, string description, string? imageUrl, string? color, string foundLocation,
        string? foundReference, string city, DateOnly dateFound)
    {
        Validation(name, description, imageUrl, color, foundLocation, foundReference, city, dateFound);
    }

    [Key]
    public int Id { get; private set; }
    
    [StringLength(40)]
    [Required(ErrorMessage="Nome é obrigatório",AllowEmptyStrings=false)]
    [Column("nome")]
    public string Name { get; private set; }
    
    [StringLength(600)]
    [Required(ErrorMessage="Descrição é obrigatória",AllowEmptyStrings=false)]
    [Column("descricao")]
    public string Description { get; private set; }
    
    [Column("imagem_url")]
    public string? ImageUrl { get; private set; }
    
    [StringLength(20,MinimumLength=3)]
    [Column("cor")]
    public string? Color { get; private set; }
    
    [StringLength(200)]
    [Required(ErrorMessage="Local encontrado é obrigatório",AllowEmptyStrings=false)]
    [Column("local_encontrado")]
    public string FoundLocation { get; private set; }
    
    [StringLength(200)]
    [Column("local_referencia")]
    public string? FoundReference { get; private set; }
    
    [StringLength(30)]
    [Required(ErrorMessage="Cidade é obrigatório",AllowEmptyStrings=false)]
    [Column("cidade")]
    public string City { get; private set; }
    
    [Required(ErrorMessage="Data encontrado é obrigatório",AllowEmptyStrings=false)]
    [Column("data_encontrado")]
    public DateOnly DateFound { get; private set; }


    private void Validation(string name, string description, string? imageUrl, string? color, string foundLocation,
        string? foundReference, string city, DateOnly? dateFound)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "Nome deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(description), "Descrição deve ser informada!");
        DomainValidationException.When(string.IsNullOrEmpty(foundLocation), "Local encontrado deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(city), "Cidade encontrado deve ser informada!");
        DomainValidationException.When(!dateFound.HasValue, "Data encontrada deve ser informada!");

        Name = name;
        Description = description;
        FoundLocation = foundLocation;
        City = city;
        DateFound = dateFound.Value;
        ImageUrl = imageUrl;
        Color = color;
        FoundReference = foundReference;
    }
}