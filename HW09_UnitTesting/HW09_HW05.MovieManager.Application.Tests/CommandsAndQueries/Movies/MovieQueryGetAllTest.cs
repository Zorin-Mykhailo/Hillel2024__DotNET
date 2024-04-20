using AutoFixture;
using HW05.MovieManager.Application.CommandsAndQueries.Movies;
using HW05.MovieManager.Domain.DTOs;
using HW05.MovieManager.Domain.Entities;
using HW05.MovieManager.Persistence.Context;

namespace HW09_HW05.MovieManager.Application.Tests.CommandsAndQueries.Movies;

public class MovieQueryGetAllTest
{
    private readonly AppDbContextDecorator<AppDbContext> _dbContext;
    protected readonly CancellationTokenSource _cts = new ();

    public MovieQueryGetAllTest()
    {
        var options = Utilites.CreateInMemoryDbOptions<AppDbContext>();
        _dbContext = new(options);
    }

    [Fact]
    public void MovieQueryGetAll_Handle()
    {
        Fixture fixture = new ();

        List<Movie> movies =
        [
            fixture.Build<Movie>().With(e => e.Id, 1).Create(),
            fixture.Build<Movie>().With(e => e.Id, 2).Create(),
            fixture.Build<Movie>().With(e => e.Id, 3).Create(),
        ];
        _dbContext.AddAndSaveRange(movies);

        MovieQueryGetAll movieQueryGetAll = fixture.Build<MovieQueryGetAll>().Create();

        _dbContext.Assert(async context =>
        {
            // Arrange
            MovieQueryGetAll.Handler sut = CreateSUT(context);

            // Act
            ICollection<MovieDTO> getAllResults = await sut.Handle(movieQueryGetAll);

            // Assert
            Assert.NotEmpty(getAllResults);
            Assert.Equal(3, getAllResults.Count);

            Movie expectedMovie = movies.First();
            MovieDTO actualMovie = getAllResults.Last();
            Assert.Multiple(() =>
            {
                Assert.Equal(expectedMovie.Id, actualMovie.Id);
                Assert.Equal(expectedMovie.Title, actualMovie.Title);
                Assert.Equal(expectedMovie.Description, actualMovie.Description);
                Assert.Equal(expectedMovie.ReleaseDate, actualMovie.ReleaseDate);
            });
        });
    }

    private static MovieQueryGetAll.Handler CreateSUT(AppDbContext context) => new(context);
}
