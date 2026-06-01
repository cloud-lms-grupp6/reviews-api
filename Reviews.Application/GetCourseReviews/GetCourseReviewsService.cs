using Reviews.Application.Abstractions.Repositories;

namespace Reviews.Application.GetCourseReviews;

public class GetCourseReviewsService(IReviewRepository reviewRepository) : IGetCourseReviewsService
{
    private readonly IReviewRepository _reviewRepository = reviewRepository;

    public async Task<GetCourseReviewsResult> GetAsync(Guid courseId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentException("Page number must be greater than 0.");
        }

        if (pageSize < 1)
        {
            throw new ArgumentException("Page size must be greater than 0.");
        }

        var skip = (pageNumber - 1) * pageSize;

        var reviews = await _reviewRepository.GetByCourseIdAsync(courseId, skip, pageSize, cancellationToken);

        var totalCount = await _reviewRepository.CountByCourseIdAsync(courseId, cancellationToken);

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new GetCourseReviewsResult
        {
            Items = reviews.Select(review => new CourseReviewDto
            {
                Id = review.Id,
                CourseId = review.CourseId,
                UserId = review.UserId,
                Rating = review.Rating.Value,
                Text = review.Text,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdatedAt
            }).ToList(),

            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }
}