namespace Reviews.Application.GetRatingSummary;

public interface IGetRatingSummaryService
{
    Task<GetRatingSummaryResult> GetSummaryAsync(Guid courseId, CancellationToken cancellationToken);
}