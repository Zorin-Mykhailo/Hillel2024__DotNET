﻿using HW05.MovieManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HW05.MovieManager.Application.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Movie> Movies { get; set; }

    DbSet<Session> Sessions { get; set; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}