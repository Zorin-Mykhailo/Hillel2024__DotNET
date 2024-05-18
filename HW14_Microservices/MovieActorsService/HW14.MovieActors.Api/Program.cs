using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "HW14 • Microservices • MovieActors",
    });
});
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(HW14.MovieActors.Service.CommandsAndQueries.Actors.ActorQueryGetAll).Assembly));
builder.Services.AddDbContext<HW14.MovieActors.Data.Context.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"),
    b => b.MigrationsAssembly(typeof(HW14.MovieActors.Data.Context.AppDbContext).Assembly.FullName)));

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HW14 • Microservices • MovieActors"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

foreach(var service in builder.Services)
{
    Console.WriteLine($"{service.Lifetime} | {service.ServiceType}");
}

app.Run();