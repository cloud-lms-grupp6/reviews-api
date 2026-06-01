using Reviews.Application.Abstractions.Repositories;

namespace Reviews.Application.GetRatingSummary;

public class GetRatingSummaryService(IReviewRepository reviewRepository) : IGetRatingSummaryService
{
    private readonly IReviewRepository _reviewRepository = reviewRepository;

    public async Task<GetRatingSummaryResult> GetSummaryAsync(Guid courseId, CancellationToken cancellationToken)
    {
        var ratings = await _reviewRepository.GetRatingsByCourseIdAsync(courseId, cancellationToken);

        var totalReviews = ratings.Count;

        if (totalReviews == 0)
        {
            return new GetRatingSummaryResult
            {
                AverageRating = 0,
                TotalReviews = 0
            };
        }

        var oneStarCount = ratings.Count(rating => rating == 1);
        var twoStarCount = ratings.Count(rating => rating == 2);
        var threeStarCount = ratings.Count(rating => rating == 3);
        var fourStarCount = ratings.Count(rating => rating == 4);
        var fiveStarCount = ratings.Count(rating => rating == 5);

        var averageRating = ratings.Average();

        var oneStarPercentage = oneStarCount * 100.0 / totalReviews;
        var twoStarPercentage = twoStarCount * 100.0 / totalReviews;
        var threeStarPercentage = threeStarCount * 100.0 / totalReviews;
        var fourStarPercentage = fourStarCount * 100.0 / totalReviews;
        var fiveStarPercentage = fiveStarCount * 100.0 / totalReviews;

        return new GetRatingSummaryResult
        {
            AverageRating = averageRating,
            TotalReviews = totalReviews,

            OneStarCount = oneStarCount,
            TwoStarCount = twoStarCount,
            ThreeStarCount = threeStarCount,
            FourStarCount = fourStarCount,
            FiveStarCount = fiveStarCount,

            OneStarPercentage = oneStarPercentage,
            TwoStarPercentage = twoStarPercentage,
            ThreeStarPercentage = threeStarPercentage,
            FourStarPercentage = fourStarPercentage,
            FiveStarPercentage = fiveStarPercentage
        };
    }
}