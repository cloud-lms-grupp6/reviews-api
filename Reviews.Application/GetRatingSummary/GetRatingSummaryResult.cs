namespace Reviews.Application.GetRatingSummary;

public sealed class GetRatingSummaryResult
{
    public double AverageRating { get; init; }
    public int TotalReviews { get; init; }

    public int OneStarCount { get; init; }
    public int TwoStarCount { get; init; }
    public int ThreeStarCount { get; init; }
    public int FourStarCount { get; init; }
    public int FiveStarCount { get; init; }

    public double OneStarPercentage { get; init; }
    public double TwoStarPercentage { get; init; }
    public double ThreeStarPercentage { get; init; }
    public double FourStarPercentage { get; init; }
    public double FiveStarPercentage { get; init; }
}