using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;

namespace Store.Data.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(m => m.Orders)
            .HasForeignKey(f => f.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);



        modelBuilder.Entity<OrderProduct>()
            .HasKey(k => new { k.OrderId, k.ProductId });

        modelBuilder.Entity<OrderProduct>()
            .HasOne(o => o.Order)
            .WithMany(m => m.Products)
            .HasForeignKey(f => f.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(o => o.Product)
            .WithMany(m => m.Orders)
            .HasForeignKey(f => f.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<Category> Categories => Set<Category>();

    public virtual DbSet<Customer> Customers => Set<Customer>();

    public virtual DbSet<Order> Orders => Set<Order>();

    public virtual DbSet<OrderProduct> Orders__Products => Set<OrderProduct>();

    public virtual DbSet<Product> Products => Set<Product>();
}



public class DesignTimeAppDbContextContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<AppDbContext> optionsBuilder = new ();
        optionsBuilder.UseSqlServer(GetAppConfig().GetConnectionString("AppDb")!);
        return new AppDbContext(optionsBuilder.Options);
    }

    private IConfigurationRoot GetAppConfig()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false);

        IConfigurationRoot config = builder.Build();
        return config;
    }
}