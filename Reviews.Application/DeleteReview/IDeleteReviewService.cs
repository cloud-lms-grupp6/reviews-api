namespace Reviews.Application.DeleteReview;

public interface IDeleteReviewService
{
    Task DeleteAsync(Guid courseId, Guid userId, CancellationToken cancellationToken);
}