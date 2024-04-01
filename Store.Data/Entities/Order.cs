using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"Orders")]
public class Order
{
    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime LastModifiedDate { get; set; }

    [Key]
    public int Id { get; set; }

    [Required]
    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public ICollection<Order__Product>? Products { get; set; }

    [Required]
    public double TotalSum { get; set; }
}
