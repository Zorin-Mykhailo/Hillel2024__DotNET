namespace Store.Contract.Requests;

public class UpsertCategoryRequest
{
    public DateTime CreatedDate { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }
}