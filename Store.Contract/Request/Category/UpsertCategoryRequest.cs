namespace Store.Contract.Request.Category;

public class UpsertCategoryRequest
{
    public DateTime CreatedDate { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }
}