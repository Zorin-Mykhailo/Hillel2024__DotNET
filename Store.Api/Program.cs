using CorrelationId;
using CorrelationId.DependencyInjection;
using Store.Api.Middleware;
using Store.Api.Modules;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddCore(builder.Configuration);
builder.Services.AddLogging();
builder.Services.AddDefaultCorrelationId();
var app = builder.Build();
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "Movie Manager API V1");
        c.OAuthAppName("Movie Manager API");
    });
}
//app.UseCorrelationId();
//app.UseMiddleware<GlobalExceptionMiddleware>();
//app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();