namespace Store.Contract.Responses;

public class CustomerResponse
{
    public DateTime CreatedDate { get; set; }

    public DateTime UdateDate { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }
}
