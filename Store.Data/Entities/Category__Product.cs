using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"Categories__Products")]
public class Category__Product
{
    [Required]
    public DateTime CreatedDate { get; set; }

    [Key, Required]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    [Key, Required]
    public int ProductId { get; set; }

    public Product? Product { get; set; }
}
