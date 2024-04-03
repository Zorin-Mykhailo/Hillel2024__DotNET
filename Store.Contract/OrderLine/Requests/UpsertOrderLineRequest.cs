using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contract.ProductInOrder.Requests;
public class UpsertOrderLineRequest
{
    public int ProductId { get; set; }
    
    public double ProductAmount { get; set; }

    public string? Notes { get; set; } = string.Empty;
}