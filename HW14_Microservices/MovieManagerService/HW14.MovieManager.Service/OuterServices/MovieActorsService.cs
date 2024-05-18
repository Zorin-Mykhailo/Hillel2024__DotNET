using HW14.MovieManager.Contract.DTOs;
using HW14.MovieManager.Service.Common;
using Microsoft.Extensions.Configuration;

namespace HW14.MovieManager.Service.OuterServices;


public interface IMovieActorsService
{
    Task<ICollection<ActorDTO>> ActorsGetAll();

    Task<ActorDTO?> ActorGetSingle(int id);
}

public class MovieActorsService : HttpClientBase, IMovieActorsService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public MovieActorsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
    }

    private Uri GetOuterServiceUri(string? relativeUri = null)
    {
        return new Uri(new Uri(_configuration.GetSection("OuterServiceEndpoints:MovieActors").Value!), relativeUri);
    }


    public async Task<ActorDTO?> ActorGetSingle(int id)
    {
        return (await Get<ActorDTO?>(_httpClient, GetOuterServiceUri($"Actor/{id}"))).Value;
    }

    public async Task<ICollection<ActorDTO>> ActorsGetAll()
    {
        return (await Get<ICollection<ActorDTO>>(_httpClient, GetOuterServiceUri("Actor"))).Value!;
    }
}
