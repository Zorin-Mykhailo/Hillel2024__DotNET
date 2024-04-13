namespace Store.Contract.Responses;

public class OrderLineResponse
{
    public DateTime CreatedDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public ProductResponse Product { get; set; } = default!;

    public string? Notes { get; set; } = string.Empty;

    public double ProductAmount { get; set; }

    public double PricePerUnit { get; set; }

    public double TotalSum { get; set; }
}
