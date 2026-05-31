namespace Reviews.Application.UpdateReview;

public interface IUpdateReviewService
{
    Task UpdateAsync(Guid courseId, Guid userId, int rating, string text, CancellationToken cancellationToken);
}