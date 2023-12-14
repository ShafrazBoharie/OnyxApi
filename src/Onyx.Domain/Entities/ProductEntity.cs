using System.ComponentModel.DataAnnotations;

namespace Onyx.Domain.Entities;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(20)]
    public string Color { get; set; }
}