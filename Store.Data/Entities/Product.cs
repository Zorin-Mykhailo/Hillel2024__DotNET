using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"Products")]
public class Product
{
    [Required]
    public DateTime CreatedDate { get; set; }

    [Key]
    public int Id { get; set; }

    [Required, MaxLength(250)]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    public double CurrentPricePerUnit { get; set; }

    public ICollection<Category__Product>? Categories { get; set; }

    public ICollection<Order__Product>? Orders { get; set; }
}
