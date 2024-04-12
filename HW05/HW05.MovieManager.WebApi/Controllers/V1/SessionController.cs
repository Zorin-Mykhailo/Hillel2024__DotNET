using Asp.Versioning;
using HW05.MovieManager.Application.CommandsAndQueries.Sessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HW05.MovieManager.WebApi.Controllers.V1;


[ApiVersion("1.0")]
public class SessionController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(SessionCommandCreate command)
    {
        throw new NotImplementedException();
    }



    [HttpPut("[action]")]
    public async Task<IActionResult> Update(int id, SessionCommandUpdateById command)
    {
        throw new NotImplementedException();
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        throw new NotImplementedException();
    }



    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        throw new NotImplementedException();
    }
}