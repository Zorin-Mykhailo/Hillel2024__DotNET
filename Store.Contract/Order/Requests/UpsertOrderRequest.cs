namespace Store.Contract.Requests;

public class UpsertOrderRequest
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? Notes { get; set; } = string.Empty;
}
