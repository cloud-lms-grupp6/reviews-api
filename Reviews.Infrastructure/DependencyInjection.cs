using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reviews.Infrastructure.Persistence;

namespace Reviews.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("ReviewsDatabase");

        if (environment.IsDevelopment())
        {
            services.AddDbContext<ReviewsDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
        }
        else
        {
            services.AddDbContext<ReviewsDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }

        return services;
    }
}