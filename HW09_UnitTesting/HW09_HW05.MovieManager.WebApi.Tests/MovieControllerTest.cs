using HW05.MovieManager.Application.CommandsAndQueries.Movies;
using HW05.MovieManager.WebApi.Controllers.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HW09_HW05.MovieManager.WebApi.Tests;

public class MovieControllerTest
{
    private CancellationTokenSource CTS { get; set; } = new();
    
    private Mock<IMediator> MockMediator { get; set; } = new();
    
    private MovieController MovieController { get; set; }

    public MovieControllerTest()
    {   
        MovieController = new MovieController(MockMediator.Object);
    }

    [Fact]
    public async Task Create_WhenCalled_ReturnsCreatedResponse()
    {
        // Arrange
        MovieCommandCreate createCommand = new()
        {
            Title = "Title",
            Description = "Description",
            ReleaseDate = DateTime.Now,
        };

        int createdEntityId = 1;
        MockMediator.Setup(m => m.Send(It.IsAny<MovieCommandCreate>(), It.IsAny<CancellationToken>())).ReturnsAsync(createdEntityId);

        // Act
        CreatedAtActionResult? actualResult = await MovieController.Create(createCommand) as CreatedAtActionResult;

        
        Assert.NotNull(actualResult);
        Assert.Equal(nameof(HW05.MovieManager.WebApi.Controllers.V1.MovieController.GetById), actualResult.ActionName);
        Assert.Equal(createdEntityId, actualResult.RouteValues["id"]);
    }
}