namespace Reviews.Application.GetCourseReviews;

public interface IGetCourseReviewsService
{
    Task<GetCourseReviewsResult> GetAsync(Guid courseId, int pageNumber, int pageSize, CancellationToken cancellationToken);
}