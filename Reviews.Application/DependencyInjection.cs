using Microsoft.Extensions.DependencyInjection;
using Reviews.Application.CreateReview;
using Reviews.Application.UpdateReview;

namespace Reviews.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateReviewService, CreateReviewService>();
        services.AddScoped<IUpdateReviewService, UpdateReviewService>();

        return services;
    }
}