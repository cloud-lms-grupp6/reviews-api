using Microsoft.Extensions.DependencyInjection;
using Reviews.Application.CreateReview;

namespace Reviews.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateReviewService, CreateReviewService>();

        return services;
    }
}