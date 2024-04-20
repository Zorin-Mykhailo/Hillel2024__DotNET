using HW05.MovieManager.Application.CommandsAndQueries.Sessions;

namespace HW09_HW05.MovieManager.Application.Tests.CommandsAndQueries.Sessions;

public class SessionQueryGetAllTest
{
    private readonly AppDbContextDecorator<AppDbContext> _dbContext;
    protected readonly CancellationTokenSource _cts = new ();

    public SessionQueryGetAllTest()
    {
        var options = Utilites.CreateInMemoryDbOptions<AppDbContext>();
        _dbContext = new(options);
    }

    private static SessionQueryGetAll.Handler CreateSUT(AppDbContext context) => new(context);

    [Fact]
    public void SessionQueryGetAll_Handle()
    {
        int relatedMovieId = 1;

        Fixture fixture = new ();

        Movie movie = fixture.Build<Movie>().With(e => e.Id, relatedMovieId).Create();
        _dbContext.AddAndSave(movie);

        List<Session> sessions =
        [
            fixture.Build<Session>().With(e => e.MovieId, relatedMovieId).With(e => e.Id, 1).Create(),
            fixture.Build<Session>().With(e => e.MovieId, relatedMovieId).With(e => e.Id, 2).Create(),
            fixture.Build<Session>().With(e => e.MovieId, relatedMovieId).With(e => e.Id, 3).Create(),
        ];
        _dbContext.AddAndSaveRange(sessions);

        SessionQueryGetAll sessionQueryGetAll = fixture.Build<SessionQueryGetAll>().Create();

        _dbContext.Assert(async context =>
        {
            // Arrange
            SessionQueryGetAll.Handler sut = CreateSUT(context);

            // Act
            ICollection<SessionDTO> getAllResults = await sut.Handle(sessionQueryGetAll);

            // Assert
            Assert.NotEmpty(getAllResults);
            Assert.Equal(sessions.Count, getAllResults.Count);

            Session expectedSession = sessions.First();
            SessionDTO actualSession = getAllResults.Last();
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