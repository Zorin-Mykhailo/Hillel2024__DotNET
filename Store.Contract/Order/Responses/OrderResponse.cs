using System.ComponentModel.DataAnnotations;

namespace Store.Contract.Responses;

public record OrderResponse(int Id, DateTime CreatedDate, DateTime UdateDate)
{
    //public DateTime CreatedDate { get; set; }

    //public DateTime UdateDate { get; set; }

    //public int Id { get; set; }

    public int CustomerId { get; set; }

    public CustomerResponse? Customer { get; set; }

    public double TotalSum { get; set; }

    public string? Notes { get; set; } = string.Empty;
}
