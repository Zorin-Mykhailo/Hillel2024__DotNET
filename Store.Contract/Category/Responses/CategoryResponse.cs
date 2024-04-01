namespace Store.Contract.Responses;

public class CategoryResponse
{
    public DateTime CreatedDate { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }

    public List<int>? ProductsId { get; set; }
}