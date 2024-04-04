namespace Store.Contract.Requests;

public record UpdateOrderRequest
{
    public string? Notes { get; set; } = string.Empty;
}