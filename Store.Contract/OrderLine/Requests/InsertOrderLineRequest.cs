namespace Store.Contract.Requests;

public record InsertOrderLineRequest
{
    public int ProductId { get; set; }

    public double ProductAmount { get; set; }

    public string? Notes { get; set; } = string.Empty;
}
