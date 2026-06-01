using Microsoft.Extensions.DependencyInjection;
using Reviews.Application.CreateReview;
using Reviews.Application.UpdateReview;
using Reviews.Application.DeleteReview;
using Reviews.Application.GetCourseReviews;
using Reviews.Application.GetRatingSummary;

namespace Reviews.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateReviewService, CreateReviewService>();
        services.AddScoped<IUpdateReviewService, UpdateReviewService>();
        services.AddScoped<IDeleteReviewService, DeleteReviewService>();
        services.AddScoped<IGetCourseReviewsService, GetCourseReviewsService>();
        services.AddScoped<IGetRatingSummaryService, GetRatingSummaryService>();

        return services;
    }
}