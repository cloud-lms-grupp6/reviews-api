namespace Reviews.Application.GetCourseReviews;

public sealed class GetCourseReviewsResult
{
    public IReadOnlyList<CourseReviewDto> Items { get; init; } = [];
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}

public sealed class CourseReviewDto
{
    public Guid Id { get; init; }
    public Guid CourseId { get; init; }
    public Guid UserId { get; init; }
    public int Rating { get; init; }
    public string Text { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}