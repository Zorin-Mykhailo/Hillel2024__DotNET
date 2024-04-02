using System.ComponentModel.DataAnnotations;

namespace Store.Contract.Responses;

public class OrderResponse
{
    public DateTime CreatedDate { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public int Id { get; set; }

    public int CustomerId { get; set; }

    public CustomerResponse? Customer { get; set; }

    //public List<ProductInOrderResponse>? ProductsInOrder { get; set; } = new ();

    public double TotalSum { get; set; }

    public string? Notes { get; set; } = string.Empty;
}
