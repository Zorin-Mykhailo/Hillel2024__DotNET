using HW05.MovieManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Persistence.Context;
internal class DbInitializer(ModelBuilder modelBuilder)
{
    public void Seed()
    {
        SeedMovies();
        SeedSessions();
    }

    private void SeedMovies()
    {
        modelBuilder.Entity<Movie>(e =>
        {
            e.HasData(new Movie
            {
                Id = 1,
                Title = "",
                ReleaseDate = DateTime.Now,
            });
        });
    }

    private void SeedSessions()
    {
        modelBuilder.Entity<Session>(e =>
        {
            e.HasData(new Session
            {
                Id = 1,
                MovieId = 1,
                RoomName = "",
                StartAt = DateTime.Now,
            });
        });
    }
}
