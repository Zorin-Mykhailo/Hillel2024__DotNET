using HW05.MovieManager.Application.CommandsAndQueries.Movies;
using HW05.MovieManager.WebApi.Controllers.V1;
using Microsoft.AspNetCore.Mvc;

namespace HW09_HW05.MovieManager.WebApi.Tests;

public class MovieControllerTest
{
    private readonly MovieController _movieController;

    public MovieControllerTest()
    {
        _movieController = new MovieController();
    }

    [Fact]
    public async void Create_WhenCalled_ReturnsCreatedResponse()
    {
        // Arrange
        MovieCommandCreate createCommand = new()
        {
            Title = "Title",
            Description = "Description",
            ReleaseDate = DateTime.Now,
        };

        // Act
        var controllerResponse = await _movieController.Create(createCommand);

        // Assert
        _ = Assert.IsType<Task<CreatedAtActionResult>>(controllerResponse as CreatedAtActionResult);
    }
}