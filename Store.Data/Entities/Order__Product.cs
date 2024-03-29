using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"Orders__Products")]
public class Order__Product
{
    [Required]
    public DateTime CreatedDate { get; set; }

    [Key, Required]
    public int OrderId { get; set; }

    public Order? Order { get; set; }

    [Key, Required]
    public int ProductId { get; set; }

    public Product? Product { get; set; }
}
