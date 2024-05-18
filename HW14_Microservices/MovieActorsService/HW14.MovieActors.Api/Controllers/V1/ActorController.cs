using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HW14.MovieActors.Api.Controllers.V1;

[ApiVersion(1.0)]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ActorController(IMediator Mediator) : ControllerBase
{
    //[HttpPost]
    //[ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
    //public async Task<IActionResult> Create(MovieCommandCreate command, CancellationToken cancellationToken = default)
    //{
    //    int createdEntityId = await Mediator.Send(command, cancellationToken);
    //    var routeValues = new { id = createdEntityId, version = new ApiVersion(1, 0).ToString()};
    //    return CreatedAtAction(nameof(GetById), routeValues, createdEntityId);
    //}



    //[HttpDelete("{id}")]
    //[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    //[ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
    //public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    //{
    //    bool success = await Mediator.Send(new MovieCommandDeleteSingle(id), cancellationToken);
    //    return success ? Ok(id) : NotFound(id);
    //}



    //[HttpPut("{id}")]
    //[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    //[ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
    //public async Task<IActionResult> Update(int id, MovieCommandUpdate command, CancellationToken cancellationToken = default)
    //{
    //    bool success = await Mediator.Send(new MovieCommandUpdateSingle(id, command), cancellationToken);
    //    return success ? Ok(id) : NotFound(id);
    //}



    //[HttpGet]
    //[ProducesResponseType(typeof(ICollection<MovieDTO>), (int)HttpStatusCode.OK)]
    //[ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    //public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    //{
    //    ICollection<MovieDTO> items = await Mediator.Send(new MovieQueryGetAll(), cancellationToken);
    //    return items.Any() ? Ok(items) : NoContent();
    //}



    //[HttpGet("{id}")]
    //[ProducesResponseType(typeof(MovieDTO), (int)HttpStatusCode.OK)]
    //[ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
    //public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    //{
    //    MovieDTO? singleItem = await Mediator.Send(new MovieQueryGetById(id), cancellationToken);
    //    return singleItem != null ? Ok(singleItem) : NotFound(id);
    //}
}
