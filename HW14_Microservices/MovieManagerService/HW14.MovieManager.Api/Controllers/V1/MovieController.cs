using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using HW14.MovieManager.Service.CommandsAndQueries.Movies;
using HW14.MovieManager.Contract.DTOs;

namespace HW14.MovieManager.Api.Controllers.V1;

[ApiVersion(1.0)]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class MovieController(IMediator Mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> Create(MovieCommandCreate command, CancellationToken cancellationToken = default)
    {
        int createdEntityId = await Mediator.Send(command, cancellationToken);
        var routeValues = new { id = createdEntityId, version = new ApiVersion(1, 0).ToString()};
        return CreatedAtAction(nameof(GetById), routeValues, createdEntityId);
    }




    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MovieDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        MovieDTO? singleItem = await Mediator.Send(new MovieQueryGetById(id), cancellationToken);
        return singleItem != null ? Ok(singleItem) : NotFound(id);
    }
}
