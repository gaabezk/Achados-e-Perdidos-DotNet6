using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities;

public abstract class BaseEntity
{
    [Key] public Guid Id { get; set; }
}