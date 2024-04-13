using Asp.Versioning;
using HW05.MovieManager.Application.CommandsAndQueries.Sessions;
using HW05.MovieManager.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HW05.MovieManager.WebApi.Controllers.V1;


[ApiVersion("1.0")]
public class SessionController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(SessionCommandCreate command)
    {
        return Ok(await Mediator.Send(command));
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await Mediator.Send(new SessionCommandDeleteById(id));
        return success ? Ok(id) : NotFound(id);
    }




    [HttpPut("[action]")]
    public async Task<IActionResult> Update(int id, SessionCommandUpdate command)
    {
        bool success = await Mediator.Send(new SessionCommandUpdateById(id, command));
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