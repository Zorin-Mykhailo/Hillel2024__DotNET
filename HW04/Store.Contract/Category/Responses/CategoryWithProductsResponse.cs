using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contract.Responses;

public class CategoryWithProductsResponse
{
    public DateTime CreatedDate { get; set; }

    public DateTime UdateDate { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }

    public List<ProductResponse>? Products { get; set; }
}