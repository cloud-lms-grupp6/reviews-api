namespace Reviews.Api.Contracts;

public sealed class CreateReviewRequest
{
    public Guid UserId { get; set; }

    public int Rating { get; set; }

    public string Text { get; set; } = string.Empty;
}