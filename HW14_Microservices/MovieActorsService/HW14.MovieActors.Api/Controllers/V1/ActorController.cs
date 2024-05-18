using Asp.Versioning;
using HW14.MovieActors.Contract.DTOs;
using HW14.MovieActors.Service.CommandsAndQueries.Actors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HW14.MovieActors.Api.Controllers.V1;

[ApiVersion(1.0)]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ActorController(IMediator Mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> Create(ActorCommandCreate command, CancellationToken cancellationToken = default)
    {
        int createdEntityId = await Mediator.Send(command, cancellationToken);
        var routeValues = new { id = createdEntityId, version = new ApiVersion(1, 0).ToString()};
        return CreatedAtAction(nameof(GetById), routeValues, createdEntityId);
    }



    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        bool success = await Mediator.Send(new ActorCommandDeleteSingle(id), cancellationToken);
        return success ? Ok(id) : NotFound(id);
    }



    [HttpPut("{id}")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update(int id, ActorCommandUpdate command, CancellationToken cancellationToken = default)
    {
        bool success = await Mediator.Send(new ActorCommandUpdateSingle(id, command), cancellationToken);
        return success ? Ok(id) : NotFound(id);
    }



    [HttpGet]
    [ProducesResponseType(typeof(ICollection<ActorDTO>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        ICollection<ActorDTO> items = await Mediator.Send(new ActorQueryGetAll(), cancellationToken);
        return items.Any() ? Ok(items) : NoContent();
    }



    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ActorDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        ActorDTO? singleItem = await Mediator.Send(new ActorQueryGetById(id), cancellationToken);
        return singleItem != null ? Ok(singleItem) : NotFound(id);
    }
}
