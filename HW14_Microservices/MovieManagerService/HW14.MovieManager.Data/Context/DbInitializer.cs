using HW14.MovieManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HW14.MovieManager.Data.Context;

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
                Title = "Приборкувачка драконів",
                ReleaseDate = new(2024, 04, 11),
            });
            e.HasData(new Movie
            {
                Id = 2,
                Title = "Повстання Штатів",
                ReleaseDate = new(2024, 04, 11),
            });
            e.HasData(new Movie
            {
                Id = 3,
                Title = "Панда Кунг-Фу 4",
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
                RoomName = "Зал 4",
                StartAt = new(2024, 04, 11, 14, 30, 00),
            });
            e.HasData(new Session
            {
                Id = 2,
                MovieId = 1,
                RoomName = "Зал 4",
                StartAt = new(2024, 04, 11, 16, 40, 00),
            });
            e.HasData(new Session
            {
                Id = 3,
                MovieId = 2,
                RoomName = "Зал 1",
                StartAt = new(2024, 04, 11, 14, 50, 00),
            });
            e.HasData(new Session
            {
                Id = 4,
                MovieId = 2,
                RoomName = "Зал 2",
                StartAt = new(2024, 04, 11, 15, 50, 00),
            });
            e.HasData(new Session
            {
                Id = 5,
                MovieId = 2,
                RoomName = "Зал 1",
                StartAt = new(2024, 04, 11, 17, 10, 00),
            });
            e.HasData(new Session
            {
                Id = 6,
                MovieId = 2,
                RoomName = "Зал 1",
                StartAt = new(2024, 04, 11, 19, 30, 00),
            });
            e.HasData(new Session
            {
                Id = 7,
                MovieId = 2,
                RoomName = "Зал 2",
                StartAt = new(2024, 04, 11, 20, 30, 00),
            });
            e.HasData(new Session
            {
                Id = 8,
                MovieId = 3,
                RoomName = "Зал 7",
                StartAt = new(2024, 04, 11, 14, 20, 00),
            });
            e.HasData(new Session
            {
                Id = 9,
                MovieId = 3,
                RoomName = "Зал 7",
                StartAt = new(2024, 04, 11, 16, 20, 00),
            });
        });
    }
}