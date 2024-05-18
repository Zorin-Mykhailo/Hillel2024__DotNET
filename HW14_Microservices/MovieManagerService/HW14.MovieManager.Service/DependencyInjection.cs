using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HW14.MovieManager.Service;
public static class DependencyInjection
{
    public static void AddServiceLayer(this IServiceCollection services)
        => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
}