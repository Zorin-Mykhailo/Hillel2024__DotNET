using AutoFixture;
using HW05.MovieManager.Application.CommandsAndQueries.Movies;
using HW05.MovieManager.Application.CommandsAndQueries.Sessions;

namespace HW09_HW05.MovieManager.Application.Tests.CommandsAndQueries.Sessions;

public class SessionCommandCreateTest
{
    private readonly AppDbContextDecorator<AppDbContext> _dbContext;
    protected readonly CancellationTokenSource _cts = new ();

    public SessionCommandCreateTest()
    {
        var options = Utilites.CreateInMemoryDbOptions<AppDbContext>();
        _dbContext = new(options);
    }

    private static SessionCommandCreate.Handler CreateSUT(AppDbContext context) => new(context);

    [Fact]
    public void SessionCommandCreate_Handle()
    {
        Fixture fixture = new ();
        
        Movie movie = fixture.Build<Movie>().With(e => e.Id, 1).Create();
        _dbContext.AddAndSave(movie);

        SessionCommandCreate sessionCommandCreate = fixture.Build<SessionCommandCreate>()
            .With(e => e.MovieId)
            .Create();

        _dbContext.Assert(async context =>
        {
            // Arrange
            SessionCommandCreate.Handler sut = CreateSUT(context);

            // Act
            int createResult = await sut.Handle(sessionCommandCreate, _cts.Token);

            // Assert
            Assert.True(createResult > 0, "Created object id is not positive number");
        });
    }
}
