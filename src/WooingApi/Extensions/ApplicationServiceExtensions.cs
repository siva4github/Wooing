using Microsoft.EntityFrameworkCore;
using WooingApi.Data;

namespace WooingApi.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        // Configure DbContext
        services.AddDbContext<DataContext>(options => options.UseSqlite());

        return services;
    }
}