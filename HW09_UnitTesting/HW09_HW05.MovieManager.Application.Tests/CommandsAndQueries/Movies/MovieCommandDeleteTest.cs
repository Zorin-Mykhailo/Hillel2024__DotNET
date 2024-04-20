using HW05.MovieManager.Application.CommandsAndQueries.Movies;

namespace HW09_HW05.MovieManager.Application.Tests.CommandsAndQueries.Movies;
public class MovieCommandDeleteTest
{
    private readonly AppDbContextDecorator<AppDbContext> _dbContext;
    protected readonly CancellationTokenSource _cts = new ();

    public MovieCommandDeleteTest()
    {
        var options = Utilites.CreateInMemoryDbOptions<AppDbContext>();
        _dbContext = new (options);
    }

    private static MovieCommandDeleteSingle.Handler CreateSUT(AppDbContext context) => new(context);

    [Fact]
    public void MovieCommandDelete_Handle()
    {
        int deletableMovieId = 1;

        Movie movie = new Fixture().Build<Movie>()
            .With(e => e.Id, deletableMovieId)
            .With(e => e.Title, "Deletable movie")
            .Create();
        _dbContext.AddAndSave(movie);

        MovieCommandDeleteSingle movieCommandDeleteSingle = new Fixture().Build<MovieCommandDeleteSingle>()
            .With(e => e.Id, deletableMovieId)
            .Create();

        _dbContext.Assert(async context =>
        {
            // Arrange
            MovieCommandDeleteSingle.Handler sut = CreateSUT(context);

            // Act
            bool firstDeleteResult = await sut.Handle(movieCommandDeleteSingle, _cts.Token);
            bool secondDeleteResult = await sut.Handle(movieCommandDeleteSingle, _cts.Token);

            // Assert
            Assert.True(firstDeleteResult, "Deletion of existing movie item should return true");
            Assert.False(secondDeleteResult, "Deletion of not existing movie item should return false");
        });
    }
}