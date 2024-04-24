using HW05.MovieManager.Application.CommandsAndQueries.Movies;
using HW05.MovieManager.Domain.DTOs;
using HW05.MovieManager.WebApi.Controllers.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HW09_HW05.MovieManager.WebApi.Tests;

public class MovieControllerTest
{
    private CancellationTokenSource CTS { get; set; } = new();

    [Fact]
    public async Task Create_WhenCalled_ReturnsCreatedResponse()
    {
        // Arrange
        int createdEntityId = 1;
        MovieCommandCreate createCommand = new()
        {
            Title = "Title",
            Description = "Description",
            ReleaseDate = DateTime.Now,
        };

        Mock<IMediator> MockMediator = new();
        MockMediator.Setup(m => m.Send(It.IsAny<MovieCommandCreate>(), It.IsAny<CancellationToken>())).ReturnsAsync(createdEntityId);
        MovieController sut = new (MockMediator.Object);

        // Act
        CreatedAtActionResult? actualResult = await sut.Create(createCommand, CTS.Token) as CreatedAtActionResult;

        // Assert
        Assert.NotNull(actualResult);
        Assert.Equal(nameof(HW05.MovieManager.WebApi.Controllers.V1.MovieController.GetById), actualResult.ActionName);

        object? routeValueId = actualResult?.RouteValues?["id"];
        Assert.Equal(createdEntityId, routeValueId);
    }



    [Fact]
    public async Task Delete_ExistingEntity_ReturnsOkAndEntityId()
    {
        // Arrange
        int deletableEntityId = 1;

        MovieCommandDeleteSingle deleteCommand = new (deletableEntityId);

        Mock<IMediator> MockMediator = new();
        MockMediator.Setup(m => m.Send(It.IsAny<MovieCommandDeleteSingle>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        MovieController sut = new (MockMediator.Object);

        // Act
        OkObjectResult? actualResult = await sut.Delete(deletableEntityId, CTS.Token) as OkObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task Delete_NotExistingEntity_ReturnsNotFoundAndEntityId()
    {
        // Arrange
        int deletableMovieId = 0;

        MovieCommandDeleteSingle deleteCommand = new (deletableMovieId);

        Mock<IMediator> MockMediator = new();
        MockMediator.Setup(m => m.Send(It.IsAny<MovieCommandDeleteSingle>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        MovieController sut = new (MockMediator.Object);

        // Act
        NotFoundObjectResult? actualResult = await sut.Delete(deletableMovieId, CTS.Token) as NotFoundObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task Update_ExistingEntity_ReturnsOkAndEntityId()
    {
        // Arrange
        int updatableMovieId = 1;
        MovieCommandUpdate updateCommand = new()
        {
            Description = "Description",
            ReleaseDate = DateTime.UtcNow,
            Title = "Title",
        };
        MovieCommandUpdateSingle updateSingleCommand = new (updatableMovieId, updateCommand);

        Mock<IMediator> MockMediator = new();
        MockMediator.Setup(m => m.Send(It.IsAny<MovieCommandUpdateSingle>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        MovieController sut = new (MockMediator.Object);

        // Act
        OkObjectResult? actualResult = await sut.Update(updatableMovieId, updateCommand, CTS.Token) as OkObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task Update_NotExistingEntity_ReturnsNotFoundAndEntityId()
    {
        // Arrange
        int updatableMovieId = 0;
        MovieCommandUpdate updateCommand = new()
        {
            Description = "Description",
            ReleaseDate = DateTime.UtcNow,
            Title = "Title",
        };
        MovieCommandUpdateSingle updateSingleCommand = new (updatableMovieId, updateCommand);

        Mock<IMediator> MockMediator = new();
        MockMediator.Setup(m => m.Send(It.IsAny<MovieCommandUpdateSingle>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        MovieController sut = new (MockMediator.Object);

        // Act
        NotFoundObjectResult? actualResult = await sut.Update(updatableMovieId, updateCommand, CTS.Token) as NotFoundObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task GetAll_WhenItemsExist_ReturnsOkAndItems()
    {
        // Arrange
        MovieQueryGetAll getAllCommand = new();

        ICollection<MovieDTO> movies = new List<MovieDTO>() { new () };
        Mock<IMediator> mockMediator = new();
        mockMediator.Setup(m => m.Send(It.IsAny<MovieQueryGetAll>(), It.IsAny<CancellationToken>())).ReturnsAsync(movies);
        MovieController sut = new (mockMediator.Object);

        // Act
        OkObjectResult? actualResult = await sut.GetAll(CTS.Token) as OkObjectResult;

        // Assert
        Assert.NotNull(actualResult);
        ICollection<MovieDTO>? resultValues = actualResult.Value as ICollection<MovieDTO>;

        Assert.NotNull(resultValues);
        Assert.True(resultValues.Any());
    }



    [Fact]
    public async Task GetAll_WhenNoItemsExist_ReturnsNoContent()
    {
        // Arrange
        MovieQueryGetAll getAllCommand = new();

        ICollection<MovieDTO> movies = new List<MovieDTO>() { };
        Mock<IMediator> mockMediator = new();
        mockMediator.Setup(m => m.Send(It.IsAny<MovieQueryGetAll>(), It.IsAny<CancellationToken>())).ReturnsAsync(movies);
        MovieController sut = new (mockMediator.Object);

        // Act
        NoContentResult? actualResult = await sut.GetAll(CTS.Token) as NoContentResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task GetById_ExistingEntity_ReturnsOkAndEntity()
    {
        // Arrange
        int movieId = 1;
        MovieQueryGetById getByIdCommand = new(movieId);

        MovieDTO movie = new () { Id = movieId };
        Mock<IMediator> mockMediator = new();
        mockMediator.Setup(m => m.Send(It.IsAny<MovieQueryGetById>(), It.IsAny<CancellationToken>())).ReturnsAsync(movie);
        MovieController sut = new (mockMediator.Object);

        // Act
        OkObjectResult? actualResult = await sut.GetById(movieId, CTS.Token) as OkObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task GetById_NotExistingEntity_ReturnsNotFoundAndEntityId()
    {
        // Arrange
        int movieId = 1;
        MovieQueryGetById getByIdCommand = new(movieId);

        Mock<IMediator> mockMediator = new();
        mockMediator.Setup(m => m.Send(It.IsAny<MovieQueryGetById>(), It.IsAny<CancellationToken>())).ReturnsAsync(default(MovieDTO));
        MovieController sut = new (mockMediator.Object);

        // Act
        NotFoundObjectResult? actualResult = await sut.GetById(movieId, CTS.Token) as NotFoundObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }
}