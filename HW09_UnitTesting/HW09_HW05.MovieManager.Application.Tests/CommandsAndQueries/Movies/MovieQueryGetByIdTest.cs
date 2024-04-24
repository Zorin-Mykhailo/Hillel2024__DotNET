using HW05.MovieManager.Application.CommandsAndQueries.Movies;

namespace HW09_HW05.MovieManager.Application.Tests.CommandsAndQueries.Movies;

public class MovieQueryGetByIdTest
{
    private readonly AppDbContextDecorator<AppDbContext> _dbContext;
    protected readonly CancellationTokenSource _cts = new ();

    public MovieQueryGetByIdTest()
    {
        var options = Utilites.CreateInMemoryDbOptions<AppDbContext>();
        _dbContext = new(options);
    }

    private static MovieQueryGetById.Handler CreateSUT(AppDbContext context) => new(context);

    [Fact]
    public void MovieQueryGetById_Handle()
    {
        int existingItemId = 1;

        Fixture fixture = new ();

        Movie expectedMovie = fixture.Build<Movie>().With(e => e.Id, existingItemId).Create();

        _dbContext.AddAndSave(expectedMovie);

        MovieQueryGetById movieQueryGetByIdExistingItem = fixture.Build<MovieQueryGetById>()
            .With(e => e.Id, existingItemId).Create();

        MovieQueryGetById movieQueryGetByIdNotExistingItem = fixture.Build<MovieQueryGetById>()
            .With(e => e.Id, 0).Create();

        _dbContext.Assert(async context =>
        {
            // Arrange
            MovieQueryGetById.Handler sut = CreateSUT(context);

            // Act
            MovieDTO? notExistingMovie = await sut.Handle(movieQueryGetByIdNotExistingItem);
            MovieDTO? actualMovie = await sut.Handle(movieQueryGetByIdExistingItem);

            // Assert
            Assert.Null(notExistingMovie);
            Assert.NotNull(actualMovie);

            Assert.Multiple(() =>
            {
                Assert.Equal(expectedMovie.Id, actualMovie.Id);
                Assert.Equal(expectedMovie.Title, actualMovie.Title);
                Assert.Equal(expectedMovie.Description, actualMovie.Description);
                Assert.Equal(expectedMovie.ReleaseDate, actualMovie.ReleaseDate);
            });
        });
    }
}