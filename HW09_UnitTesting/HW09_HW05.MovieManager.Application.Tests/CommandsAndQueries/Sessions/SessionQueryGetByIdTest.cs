using HW05.MovieManager.Application.CommandsAndQueries.Sessions;

namespace HW09_HW05.MovieManager.Application.Tests.CommandsAndQueries.Sessions;

public class SessionQueryGetByIdTest
{
    private readonly AppDbContextDecorator<AppDbContext> _dbContext;
    protected readonly CancellationTokenSource _cts = new ();

    public SessionQueryGetByIdTest()
    {
        var options = Utilites.CreateInMemoryDbOptions<AppDbContext>();
        _dbContext = new(options);
    }

    private static SessionQueryGetById.Handler CreateSUT(AppDbContext context) => new(context);

    [Fact]
    public void SessionQueryGetById_Handle()
    {
        int relatedMovieId = 1;
        int sessionId = 1;

        Fixture fixture = new ();

        Movie movie = fixture.Build<Movie>().With(e => e.Id, relatedMovieId).Create();
        _dbContext.AddAndSave(movie);

        Session expectedSession = fixture.Build<Session>().With(e => e.MovieId, relatedMovieId).With(e => e.Id, sessionId).Create();
        _dbContext.AddAndSave(expectedSession);

        SessionQueryGetById sessionQueryGetById_NotExistingItem = fixture.Build<SessionQueryGetById>()
            .With(e => e.Id, 0).Create();

        SessionQueryGetById sessionQueryGetById_ExistingItem = fixture.Build<SessionQueryGetById>()
            .With(e => e.Id, sessionId).Create();

        _dbContext.Assert(async context =>
        {
            // Arrange
            SessionQueryGetById.Handler sut = CreateSUT(context);

            // Act
            SessionDTO? notExistingSession = await sut.Handle(sessionQueryGetById_NotExistingItem, _cts.Token);
            SessionDTO? actualSession = await sut.Handle(sessionQueryGetById_ExistingItem, _cts.Token);

            // Assert
            Assert.Null(notExistingSession);
            Assert.NotNull(actualSession);

            Assert.Multiple(() =>
            {
                Assert.Equal(expectedSession.Id, actualSession.Id);
                Assert.Equal(expectedSession.MovieId, actualSession.MovieId);
                Assert.Equal(expectedSession.RoomName, actualSession.RoomName);
                Assert.Equal(expectedSession.StartAt, actualSession.StartAt);
            });
        });
    }
}