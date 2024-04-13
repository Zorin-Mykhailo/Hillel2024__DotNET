using Microsoft.EntityFrameworkCore;
using Store.Data.Db;
using Store.Service;

namespace Store.Api.Modules;

public static class CoreModule
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.Scan(scan => scan
           .FromAssembliesOf(typeof(IRequestHandler<>))
           .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,,>)))
               .AsImplementedInterfaces()
               .WithTransientLifetime()
           .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
               .AsImplementedInterfaces()
               .WithTransientLifetime()
           .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
               .AsImplementedInterfaces()
               .WithTransientLifetime());

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AppDb"));
        });

        return services;
    }
}