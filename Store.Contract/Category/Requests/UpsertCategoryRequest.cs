namespace Store.Contract.Requests;

public class UpsertCategoryRequest
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }
}