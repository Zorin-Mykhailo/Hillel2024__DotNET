using Asp.Versioning;
using HW05.MovieManager.Application.CommandsAndQueries.Sessions;
using HW05.MovieManager.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HW05.MovieManager.WebApi.Controllers.V1;


[ApiVersion(1.0)]
public class SessionController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(SessionCommandCreate command)
    {
        int createdEntityId = await Mediator.Send(command);
        var routeValues = new { id = createdEntityId, version = new ApiVersion(1, 0).ToString()};
        return CreatedAtAction(nameof(GetById), routeValues, createdEntityId);

        return Ok(await Mediator.Send(command));
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await Mediator.Send(new SessionCommandDeleteSingle(id));
        return success ? Ok(id) : NotFound(id);
    }




    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SessionCommandUpdate command)
    {
        bool success = await Mediator.Send(new SessionCommandUpdateSingle(id, command));
        return success ? Ok(id) : NotFound(id);
    }



    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        ICollection<SessionDTO> items = await Mediator.Send(new SessionQueryGetAll());
        return items.Any() ? Ok(items) : NoContent();
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        SessionDTO? singleItem = await Mediator.Send(new SessionQueryGetById(id));
        return singleItem != null ? Ok(singleItem) : NotFound(id);
    }
}