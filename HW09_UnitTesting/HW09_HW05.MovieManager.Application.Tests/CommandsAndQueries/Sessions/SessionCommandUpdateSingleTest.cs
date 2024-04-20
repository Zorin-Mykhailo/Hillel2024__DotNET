using HW05.MovieManager.Application.CommandsAndQueries.Sessions;

namespace HW09_HW05.MovieManager.Application.Tests.CommandsAndQueries.Sessions;

public class SessionCommandUpdateSingleTest
{
    private readonly AppDbContextDecorator<AppDbContext> _dbContext;
    protected readonly CancellationTokenSource _cts = new ();

    public SessionCommandUpdateSingleTest()
    {
        var options = Utilites.CreateInMemoryDbOptions<AppDbContext>();
        _dbContext = new(options);
    }

    private static SessionCommandUpdateSingle.Handler CreateSUT(AppDbContext context) => new(context);

    [Fact]
    public void SessionCommandUpdateSingle_Handle()
    {
        int relatedMovieId = 1;
        int updatableSessionId = 1;

        Fixture fixture = new ();

        Movie movie = fixture.Build<Movie>().With(e => e.Id, relatedMovieId).Create();
        _dbContext.AddAndSave(movie);

        Session session = fixture.Build<Session>()
            .With(e => e.Id, updatableSessionId)
            .With(e => e.MovieId, relatedMovieId).Create();
        _dbContext.AddAndSave(session);

        SessionCommandUpdateSingle sessionCommandUpdateSingle_ExistingItem = fixture.Build<SessionCommandUpdateSingle>()
            .With(e => e.Id, updatableSessionId)
            .Create();

        SessionCommandUpdateSingle sessionCommandUpdateSingle_NotExistingItem = fixture.Build<SessionCommandUpdateSingle>()
            .With(e => e.Id, 0)
            .Create();

        _dbContext.Assert(async context =>
        {
            // Arrange
            SessionCommandUpdateSingle.Handler sut = CreateSUT(context);

            // Act
            bool updateExistingItemResult = await sut.Handle(sessionCommandUpdateSingle_ExistingItem, _cts.Token);
            bool updateNotExistingItemResult = await sut.Handle(sessionCommandUpdateSingle_NotExistingItem, _cts.Token);

            // Assert
            Assert.True(updateExistingItemResult, "Esisting session item was not updated");
            Assert.False(updateNotExistingItemResult, "Result of updating not existing item is not false");
        });
    }
}