using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace HW05.MovieManager.Persistence.Context;
public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        new DbInitializer(modelBuilder).Seed();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        =>  await base.SaveChangesAsync(cancellationToken);

    public DbSet<Movie> Movies { get; set; }
    
    public DbSet<Session> Sessions { get; set; }
}