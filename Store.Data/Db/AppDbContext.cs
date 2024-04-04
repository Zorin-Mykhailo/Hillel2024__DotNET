using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;

namespace Store.Data.Db;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        new DbInitializer(modelBuilder).Seed();
    }

    public virtual DbSet<Category> Categories => Set<Category>();

    public virtual DbSet<Customer> Customers => Set<Customer>();

    public virtual DbSet<Order> Orders => Set<Order>();

    public virtual DbSet<OrderLine> OrderLines => Set<OrderLine>();

    public virtual DbSet<Product> Products => Set<Product>();
}



//public class DesignTimeAppDbContextContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//{
//    public AppDbContext CreateDbContext(string[] args)
//    {
//        DbContextOptionsBuilder<AppDbContext> optionsBuilder = new ();
//        optionsBuilder.UseSqlServer(GetAppConfig().GetConnectionString("AppDb")!);
//        return new AppDbContext(optionsBuilder.Options);
//    }

//    private IConfigurationRoot GetAppConfig()
//    {
//        IConfigurationBuilder builder = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.Development.json", optional: false);

//        IConfigurationRoot config = builder.Build();
//        return config;
//    }
//}