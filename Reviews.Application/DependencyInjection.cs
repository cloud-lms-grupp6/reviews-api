using Microsoft.Extensions.DependencyInjection;
using Reviews.Application.CreateReview;
using Reviews.Application.UpdateReview;
using Reviews.Application.DeleteReview;

namespace Reviews.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateReviewService, CreateReviewService>();
        services.AddScoped<IUpdateReviewService, UpdateReviewService>();
        services.AddScoped<IDeleteReviewService, DeleteReviewService>();

        return services;
    }
}