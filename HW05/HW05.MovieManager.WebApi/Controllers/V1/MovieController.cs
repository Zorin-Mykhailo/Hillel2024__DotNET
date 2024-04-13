using Asp.Versioning;
using HW05.MovieManager.Application.CommandsAndQueries.Movies;
using HW05.MovieManager.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HW05.MovieManager.WebApi.Controllers.V1;

[ApiVersion("1.0")]
public class MovieController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(MovieCommandCreate command)
    {
        return Ok(await Mediator.Send(command));
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await Mediator.Send(new MovieCommandDeleteSingle(id));
        return success ? Ok(id) : NotFound(id);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MovieCommandUpdate command)
    {
        bool success = await Mediator.Send(new MovieCommandUpdateSingle(id, command));
        return success ? Ok(id) : NotFound(id);
    }



    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        ICollection<MovieDTO> items = await Mediator.Send(new MovieQueryGetAll());
        return items.Any() ? Ok(items) : NoContent();
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        MovieDTO? singleItem = await Mediator.Send(new MovieQueryGetById(id));
        return singleItem != null ? Ok(singleItem) : NotFound(id);
    }
}