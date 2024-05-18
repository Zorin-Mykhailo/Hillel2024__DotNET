using Asp.Versioning;
using HW14.MovieManager.Contract.DTOs;
using HW14.MovieManager.Service.OuterServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HW14.MovieManager.Api.Controllers.V1;


[ApiVersion(1.0)]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ActorController(IMovieActorsService MovieActorsService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<ActorDTO>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        ICollection<ActorDTO> items = await MovieActorsService.ActorsGetAll();
        return items.Any() ? Ok(items) : NoContent();
    }



    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ActorDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        ActorDTO? singleItem = await MovieActorsService.ActorGetSingle(id);
        return singleItem != null ? Ok(singleItem) : NotFound(id);
    }
}