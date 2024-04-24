using HW05.MovieManager.Application.CommandsAndQueries.Sessions;

namespace HW09_HW05.MovieManager.Application.Tests.CommandsAndQueries.Sessions;

public class SessionCommandDeleteSingleTest
{
    private readonly AppDbContextDecorator<AppDbContext> _dbContext;
    protected readonly CancellationTokenSource _cts = new ();

    public SessionCommandDeleteSingleTest()
    {
        var options = Utilites.CreateInMemoryDbOptions<AppDbContext>();
        _dbContext = new(options);
    }

    private static SessionCommandDeleteSingle.Handler CreateSUT(AppDbContext context) => new(context);

    [Fact]
    public void SessionCommandDeleteSingle_Handle()
    {
        int relatedMovieId = 1;
        int deletableSessionId = 1;
        
        Fixture fixture = new ();

        Movie movie = fixture.Build<Movie>().With(e => e.Id, relatedMovieId).Create();
        _dbContext.AddAndSave(movie);

        Session session = fixture.Build<Session>()
            .With(e => e.Id, deletableSessionId)
            .With(e => e.MovieId, relatedMovieId).Create();
        _dbContext.AddAndSave(session);

        SessionCommandDeleteSingle sessionCommandDeleteSingle = fixture.Build<SessionCommandDeleteSingle>()
            .With(e => e.Id, deletableSessionId)
            .Create();

        _dbContext.Assert(async context =>
        {
            // Arrange
            SessionCommandDeleteSingle.Handler sut = CreateSUT(context);

            // Act
            bool firstDeleteResult = await sut.Handle(sessionCommandDeleteSingle, _cts.Token);
            bool secondDeleteResult = await sut.Handle(sessionCommandDeleteSingle, _cts.Token);

            // Assert
            Assert.True(firstDeleteResult, "Deletion of existing session should return true");
            Assert.False(secondDeleteResult, "Deletion of not existing session item should return false");
        });
    }
}