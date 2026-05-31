using Reviews.Application.Abstractions.Repositories;

namespace Reviews.Application.UpdateReview;

public class UpdateReviewService(IReviewRepository reviewRepository) : IUpdateReviewService
{
    private readonly IReviewRepository _reviewRepository = reviewRepository;

    public async Task UpdateAsync(Guid courseId, Guid userId, int rating, string text, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetByCourseAndUserAsync(courseId, userId, cancellationToken);

        if (review is null)
        {
            throw new InvalidOperationException(
                "Review was not found for this user and course.");
        }

        review.UpdateReview(rating, text);

        await _reviewRepository.SaveChangesAsync(cancellationToken);
    }
}