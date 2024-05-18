using HW14.MovieManager.Data.Entities;
using HW14.MovieManager.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW14.MovieManager.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        new DbInitializer(modelBuilder).Seed();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => await base.SaveChangesAsync(cancellationToken);

    public DbSet<Movie> Movies { get; set; }

    public DbSet<Session> Sessions { get; set; }
}