namespace Store.Contract.Responses;

public class OrderResponse
{
    public DateTime CreatedDate { get; set; }

    public DateTime UdateDate { get; set; }

    public int Id { get; set; }

    public int CustomerId { get; set; }

    public CustomerResponse? Customer { get; set; }

    public double TotalSum { get; set; }

    public string? Notes { get; set; } = string.Empty;
}
