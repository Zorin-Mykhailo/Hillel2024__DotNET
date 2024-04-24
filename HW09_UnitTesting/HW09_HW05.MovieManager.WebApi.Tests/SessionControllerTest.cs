using HW05.MovieManager.Application.CommandsAndQueries.Movies;
using HW05.MovieManager.Application.CommandsAndQueries.Sessions;
using HW05.MovieManager.Domain.DTOs;
using HW05.MovieManager.WebApi.Controllers.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace HW09_HW05.MovieManager.WebApi.Tests;

public class SessionControllerTest
{
    private CancellationTokenSource CTS { get; set; } = new();


    [Fact]
    public async Task Create_WhenCalled_ReturnsCreatedResponse()
    {
        // Arrange
        int createdEntityId = 1;
        SessionCommandCreate createCommand = new() { MovieId = 1 };

        Mock<IMediator> MockMediator = new();
        MockMediator.Setup(m => m.Send(It.IsAny<SessionCommandCreate>(), It.IsAny<CancellationToken>())).ReturnsAsync(createdEntityId);
        SessionController sut = new (MockMediator.Object);

        // Act
        CreatedAtActionResult? actualResult = await sut.Create(createCommand, CTS.Token) as CreatedAtActionResult;

        // Assert
        Assert.NotNull(actualResult);
        Assert.Equal(nameof(HW05.MovieManager.WebApi.Controllers.V1.SessionController.GetById), actualResult.ActionName);

        object? routeValueId = actualResult?.RouteValues?["id"];
        Assert.Equal(createdEntityId, routeValueId);
    }



    [Fact]
    public async Task Delete_ExistingEntity_ReturnsOkAndEntityId()
    {
        // Arrange
        int deletableEntityId = 1;

        SessionCommandDeleteSingle deleteCommand = new (deletableEntityId);

        Mock<IMediator> MockMediator = new();
        MockMediator.Setup(m => m.Send(It.IsAny<SessionCommandDeleteSingle>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        SessionController sut = new (MockMediator.Object);

        // Act
        OkObjectResult? actualResult = await sut.Delete(deletableEntityId, CTS.Token) as OkObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task Delete_NotExistingEntity_ReturnsNotFoundAndEntityId()
    {
        // Arrange
        int deletableEntityId = 0;

        SessionCommandDeleteSingle deleteCommand = new (deletableEntityId);

        Mock<IMediator> MockMediator = new();
        MockMediator.Setup(m => m.Send(It.IsAny<SessionCommandDeleteSingle>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        SessionController sut = new (MockMediator.Object);

        // Act
        NotFoundObjectResult? actualResult = await sut.Delete(deletableEntityId, CTS.Token) as NotFoundObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task GetAll_WhenItemsExist_ReturnsOkAndItems()
    {
        // Arrange
        SessionQueryGetAll getAllCommand = new();

        ICollection<SessionDTO> entities = new List<SessionDTO>() { new () };
        Mock<IMediator> mockMediator = new();
        mockMediator.Setup(m => m.Send(It.IsAny<SessionQueryGetAll>(), It.IsAny<CancellationToken>())).ReturnsAsync(entities);
        SessionController sut = new (mockMediator.Object);

        // Act
        OkObjectResult? actualResult = await sut.GetAll(CTS.Token) as OkObjectResult;

        // Assert
        Assert.NotNull(actualResult);
        ICollection<SessionDTO>? resultValues = actualResult.Value as ICollection<SessionDTO>;

        Assert.NotNull(resultValues);
        Assert.True(resultValues.Any());
    }



    [Fact]
    public async Task GetAll_WhenNoItemsExist_ReturnsNoContent()
    {
        // Arrange
        SessionQueryGetAll getAllCommand = new();

        ICollection<SessionDTO> entities = new List<SessionDTO>() { };
        Mock<IMediator> mockMediator = new();
        mockMediator.Setup(m => m.Send(It.IsAny<SessionQueryGetAll>(), It.IsAny<CancellationToken>())).ReturnsAsync(entities);
        SessionController sut = new (mockMediator.Object);

        // Act
        NoContentResult? actualResult = await sut.GetAll(CTS.Token) as NoContentResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task GetById_ExistingEntity_ReturnsOkAndEntity()
    {
        // Arrange
        int entityId = 1;
        SessionQueryGetById getByIdCommand = new(entityId);

        SessionDTO entity = new () { Id = entityId };
        Mock<IMediator> mockMediator = new();
        mockMediator.Setup(m => m.Send(It.IsAny<SessionQueryGetById>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        SessionController sut = new (mockMediator.Object);

        // Act
        OkObjectResult? actualResult = await sut.GetById(entityId, CTS.Token) as OkObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task GetById_NotExistingEntity_ReturnsNotFoundAndEntityId()
    {
        // Arrange
        int entityId = 1;
        SessionQueryGetById getByIdCommand = new(entityId);

        Mock<IMediator> mockMediator = new();
        mockMediator.Setup(m => m.Send(It.IsAny<SessionQueryGetById>(), It.IsAny<CancellationToken>())).ReturnsAsync(default(SessionDTO));
        SessionController sut = new (mockMediator.Object);

        // Act
        NotFoundObjectResult? actualResult = await sut.GetById(entityId, CTS.Token) as NotFoundObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task Update_ExistingEntity_ReturnsOkAndEntityId()
    {
        // Arrange
        int updatableEntityId = 1;
        SessionCommandUpdate updateCommand = new()
        {
            RoomName = "RoomName",
            StartAt = DateTime.Now
        };
        SessionCommandUpdateSingle updateSingleCommand = new (updatableEntityId, updateCommand);

        Mock<IMediator> MockMediator = new();
        MockMediator.Setup(m => m.Send(It.IsAny<SessionCommandUpdateSingle>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        SessionController sut = new (MockMediator.Object);

        // Act
        OkObjectResult? actualResult = await sut.Update(updatableEntityId, updateCommand, CTS.Token) as OkObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }



    [Fact]
    public async Task Update_NotExistingEntity_ReturnsNotFoundAndEntityId()
    {
        // Arrange
        int updatableEntityId = 1;
        SessionCommandUpdate updateCommand = new()
        {
            RoomName = "RoomName",
            StartAt = DateTime.Now
        };
        SessionCommandUpdateSingle updateSingleCommand = new (updatableEntityId, updateCommand);

        Mock<IMediator> MockMediator = new();
        MockMediator.Setup(m => m.Send(It.IsAny<SessionCommandUpdateSingle>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        SessionController sut = new (MockMediator.Object);

        // Act
        NotFoundObjectResult? actualResult = await sut.Update(updatableEntityId, updateCommand, CTS.Token) as NotFoundObjectResult;

        // Assert
        Assert.NotNull(actualResult);
    }
}