using Microsoft.EntityFrameworkCore;

namespace HW09_HW05.MovieManager.Application.Tests;
public class Utilites
{
    public static DbContextOptions<TContext> CreateInMemoryDbOptions<TContext>() where TContext : DbContext
    {
        return new DbContextOptionsBuilder<TContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }
}
