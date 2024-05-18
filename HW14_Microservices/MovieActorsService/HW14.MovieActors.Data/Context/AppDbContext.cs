using HW14.MovieActors.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HW14.MovieActors.Data.Context;

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

    public DbSet<Actor> Actors { get; set; }
}
