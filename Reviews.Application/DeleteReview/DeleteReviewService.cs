using Reviews.Application.Abstractions.Repositories;

namespace Reviews.Application.DeleteReview;

public class DeleteReviewService(IReviewRepository reviewRepository) : IDeleteReviewService
{
    private readonly IReviewRepository _reviewRepository = reviewRepository;

    public async Task DeleteAsync(Guid courseId, Guid userId, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetByCourseAndUserAsync(courseId, userId, cancellationToken);

        if (review is null)
        {
            throw new InvalidOperationException(
                "Review was not found for this user and course.");
        }

        await _reviewRepository.DeleteAsync(review, cancellationToken);

        await _reviewRepository.SaveChangesAsync(
            cancellationToken);
    }
}