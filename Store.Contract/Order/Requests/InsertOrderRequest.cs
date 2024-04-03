namespace Store.Contract.Requests;

public record InsertOrderRequest
{
    public int CustomerId { get; set; }

    public string? Notes { get; set; } = string.Empty;
}