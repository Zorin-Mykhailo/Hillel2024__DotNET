using HW05.MovieManager.Application.CommandsAndQueries.Movies;

namespace HW09_HW05.MovieManager.Application.Tests.CommandsAndQueries.Movies;

public class MovieCommandCreateTest
{
    private readonly AppDbContextDecorator<AppDbContext> _dbContext;
    protected readonly CancellationTokenSource _cts = new ();

    public MovieCommandCreateTest()
    {
        var options = Utilites.CreateInMemoryDbOptions<AppDbContext>();
        _dbContext = new (options);
    }

    private static MovieCommandCreate.Handler CreateSUT(AppDbContext context) => new(context);

    [Fact]
    public void MovieCommandCreate_Handle()
    {
        Fixture fixture = new ();
        MovieCommandCreate movieCommandCreate = fixture.Build<MovieCommandCreate>()
            .With(e => e.Title, "New movie")
            .Create();

        _dbContext.Assert(async context =>
        {
            // Arrange
            MovieCommandCreate.Handler sut = CreateSUT(context);

            // Act
            int createResult = await sut.Handle(movieCommandCreate, _cts.Token);

            // Assert
            Assert.True(createResult > 0, "Created object id is not positive number");
        });
    }
}