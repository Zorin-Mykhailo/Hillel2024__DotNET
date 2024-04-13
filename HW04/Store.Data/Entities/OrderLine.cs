using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"{nameof(OrderLine)}s")]
public record OrderLine : BaseRecord
{
    public int OrderId { get; set; }

    public Order? Order { get; set; }

    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public string? Notes { get; set; } = string.Empty;

    public double ProductAmount { get; set; }

    public double PricePerUnit { get; set; }

    public double TotalSum  { get; set; }
}



internal class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> e)
    {
        e.Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow)
            .ValueGeneratedOnAdd();
        e.Property(e => e.UpdateDate)
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow)
            .ValueGeneratedOnAddOrUpdate();
        e.HasKey(k => new { k.OrderId, k.ProductId });
        e.HasOne(o => o.Order)
            .WithMany(m => m.OrderLines)
            .HasForeignKey(f => f.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        e.HasOne(o => o.Product)
            .WithMany(m => m.Orders)
            .HasForeignKey(f => f.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        e.Property(e => e.ProductAmount).IsRequired();
        e.Property(e => e.PricePerUnit).IsRequired();
        e.Property(e => e.TotalSum).IsRequired();
    }
}