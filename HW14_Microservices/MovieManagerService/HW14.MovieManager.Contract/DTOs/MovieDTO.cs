﻿using HW14.MovieManager.Data.Entities;

namespace HW14.MovieManager.Contract.DTOs;

public record MovieDTO
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    public static MovieDTO? FromEntity(Movie? entity)
    {
        if(entity == null) return null;

        return new()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            ReleaseDate = entity.ReleaseDate,
        };
    }
}