using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using HW14.MovieManager.Service.CommandsAndQueries.Sessions;
using HW14.MovieManager.Contract.DTOs;

namespace HW14.MovieManager.Api.Controllers.V1;

[ApiVersion(1.0)]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class SessionController(IMediator Mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> Create(SessionCommandCreate command, CancellationToken cancellationToken = default)
    {
        int createdEntityId = await Mediator.Send(command, cancellationToken);
        var routeValues = new { id = createdEntityId, version = new ApiVersion(1, 0).ToString()};
        return CreatedAtAction(nameof(GetById), routeValues, createdEntityId);
    }



    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        bool success = await Mediator.Send(new SessionCommandDeleteSingle(id), cancellationToken);
        return success ? Ok(id) : NotFound(id);
    }




    [HttpPut("{id}")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update(int id, SessionCommandUpdate command, CancellationToken cancellationToken = default)
    {
        bool success = await Mediator.Send(new SessionCommandUpdateSingle(id, command), cancellationToken);
        return success ? Ok(id) : NotFound(id);
    }



    [HttpGet]
    [ProducesResponseType(typeof(ICollection<SessionDTO>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        ICollection<SessionDTO> items = await Mediator.Send(new SessionQueryGetAll(), cancellationToken);
        return items.Any() ? Ok(items) : NoContent();
    }



    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SessionDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        SessionDTO? singleItem = await Mediator.Send(new SessionQueryGetById(id), cancellationToken);
        return singleItem != null ? Ok(singleItem) : NotFound(id);
    }
}