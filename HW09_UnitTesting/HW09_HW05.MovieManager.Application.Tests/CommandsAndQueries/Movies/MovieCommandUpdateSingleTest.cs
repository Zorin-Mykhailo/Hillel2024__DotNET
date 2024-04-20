using AutoFixture;
using HW05.MovieManager.Application.CommandsAndQueries.Movies;
using HW05.MovieManager.Persistence.Context;
using HW05.MovieManager.Domain.Entities;

namespace HW09_HW05.MovieManager.Application.Tests.CommandsAndQueries.Movies;

public class MovieCommandUpdateSingleTest
{
    private readonly AppDbContextDecorator<AppDbContext> _dbContext;
    protected readonly CancellationTokenSource _cts = new ();

    public MovieCommandUpdateSingleTest()
    {
        var options = Utilites.CreateInMemoryDbOptions<AppDbContext>();
        _dbContext = new(options);
    }


    [Fact]
    public void MovieCommandUpdateSingle_Handle()
    {
        int updatableMovieId = 1;
        
        Movie movie = new Fixture().Build<Movie>()
            .With(e => e.Id, updatableMovieId)
            .Create();
        _dbContext.AddAndSave(movie);

        MovieCommandUpdateSingle movieCommandUpdateSingle_ExistingItem = new Fixture().Build<MovieCommandUpdateSingle>()
            .With(e => e.Id, updatableMovieId)
            .Create();

        MovieCommandUpdateSingle movieCommandUpdateSingle_NotExistingItem = new Fixture().Build<MovieCommandUpdateSingle>()
            .With(e => e.Id, 0)
            .Create();

        _dbContext.Assert(async context =>
        {
            // Arrange
            MovieCommandUpdateSingle.Handler sut = CreateSUT(context);

            // Act
            bool updateExistingItemResult = await sut.Handle(movieCommandUpdateSingle_ExistingItem, _cts.Token);
            bool updateNotExistingItemResult = await sut.Handle(movieCommandUpdateSingle_NotExistingItem, _cts.Token);

            // Assert
            Assert.True(updateExistingItemResult, "Existing movie item was not updated");
            Assert.False(updateNotExistingItemResult, "Result of updating not existing item is not false");
        });
    }

    private static MovieCommandUpdateSingle.Handler CreateSUT(AppDbContext context) => new(context);
}
