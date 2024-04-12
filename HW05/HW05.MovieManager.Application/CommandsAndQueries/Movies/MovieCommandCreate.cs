﻿using HW05.MovieManager.Application.Interfaces;
using HW05.MovieManager.Domain.Entities;
using MediatR;

namespace HW05.MovieManager.Application.CommandsAndQueries.Movies;

public class MovieCommandCreate : IRequest<int>
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    public class Handler(IAppDbContext appDbContext) : IRequestHandler<MovieCommandCreate, int>
    {
        public async Task<int> Handle(MovieCommandCreate command, CancellationToken cancellationToken = default)
        {
            Movie movie = new ()
            {
                Title = command.Title,
                Description = command.Description,
                ReleaseDate = command.ReleaseDate,
            };

            appDbContext.Movies.Add(movie);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }
    }
}