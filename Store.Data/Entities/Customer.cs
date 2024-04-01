using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"Customers")]
public class Customer
{
    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime LastModifiedDate { get; set; }

    [Key]
    public int Id { get; set; }

    [Required, MaxLength(250)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<Order>? Orders { get; set; }
}
