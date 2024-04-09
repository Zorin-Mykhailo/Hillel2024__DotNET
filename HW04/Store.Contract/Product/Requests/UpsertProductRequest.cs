namespace Store.Contract.Product.Requests;

public class UpsertProductRequest
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }

    public double CurrentPricePerUnit { get; set; }

    public int CategoryId { get; set; }
}
