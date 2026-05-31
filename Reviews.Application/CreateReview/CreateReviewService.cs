using Reviews.Application.Abstractions.Repositories;
using Reviews.Domain.Entities;

namespace Reviews.Application.CreateReview;

public class CreateReviewService(IReviewRepository reviewRepository) : ICreateReviewService
{
    private readonly IReviewRepository _reviewRepository = reviewRepository;

    public async Task<Review> CreateAsync(Guid courseId, Guid userId, int rating, string text, CancellationToken cancellationToken)
    {
        var reviewAlreadyExists =
            await _reviewRepository.ExistsForCourseAndUserAsync(courseId, userId, cancellationToken);

        if (reviewAlreadyExists)
        {
            throw new InvalidOperationException("User has already reviewed this course.");
        }

        var review = new Review(courseId, userId, rating, text);

        await _reviewRepository.AddAsync(review, cancellationToken);
        await _reviewRepository.SaveChangesAsync(cancellationToken);

        return review;
    }
}