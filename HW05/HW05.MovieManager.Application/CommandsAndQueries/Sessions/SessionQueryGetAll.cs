using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.CommandsAndQueries.Sessions;

public class SessionQueryGetAll : IRequest<IEnumerable<SessionDTO>>
{
    public class Handler(IAppDbContext appDbContext) : IRequestHandler<SessionQueryGetAll, IEnumerable<SessionDTO>>
    {
        public async Task<IEnumerable<SessionDTO>> Handle(SessionQueryGetAll query, CancellationToken cancellationToken = default)
        {
            IEnumerable<SessionDTO> sessions = await appDbContext.Sessions.Select(e => SessionDTO.FromEntity(e)!).ToListAsync(cancellationToken);
            return sessions;
        }
    }
}