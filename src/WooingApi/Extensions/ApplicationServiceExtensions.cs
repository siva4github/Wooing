using Microsoft.EntityFrameworkCore;
using WooingApi.Data;
using WooingApi.Profiles;

namespace WooingApi.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {

        // Configure DbContext
        services.AddDbContext<DataContext>(options => options.UseSqlite(config.GetConnectionString("WooingSqliteDB")));

        // AutoMapper
        services.AddAutoMapper(typeof(WooingProfiles).Assembly);

        return services;
    }
}