using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"Products")]
public class Product
{
    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime LastModifiedDate { get; set; }

    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    [Required]
    public double CurrentPricePerUnit { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = default!;

    public ICollection<ProductInOrder>? Orders { get; set; }
}
