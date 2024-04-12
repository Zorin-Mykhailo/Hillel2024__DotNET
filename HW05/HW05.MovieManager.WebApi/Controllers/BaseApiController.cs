using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HW05.MovieManager.WebApi.Controllers;


[ApiController]
//[Route("api/v{version:apiVersion}/[controller]")]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator _mediator = default!;

    protected IMediator Medeator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}
