using Microsoft.EntityFrameworkCore;
using HW14.MovieManager.Data.Entities;

namespace HW14.MovieManager.Data.Interfaces;

public interface IAppDbContext
{
    DbSet<Movie> Movies { get; set; }

    DbSet<Session> Sessions { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
