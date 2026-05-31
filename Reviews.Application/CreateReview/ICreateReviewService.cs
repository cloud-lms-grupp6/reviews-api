using Reviews.Domain.Entities;

namespace Reviews.Application.CreateReview;

public interface ICreateReviewService
{
    Task<Review> CreateAsync(Guid courseId, Guid userId, int rating, string text, CancellationToken cancellationToken);
}