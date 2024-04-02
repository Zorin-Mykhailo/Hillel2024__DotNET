using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"OrdersProducts")]
public class OrderProduct
{
    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime LastModifiedDate { get; set; }

    [Key, Required]
    public int OrderId { get; set; }

    public Order? Order { get; set; }

    [Key, Required]
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public string? Notes { get; set; } = string.Empty;

    [Required]
    public double ProductAmount { get; set; }

    [Required]
    public double PricePerUnit { get; set; }

    [Required]
    public double TotalSum  { get; set; }
}
