namespace Reviews.Domain.Entities;


public class Review
{
    public Guid Id { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid UserId { get; private set; }
    public Rating Rating { get; private set; } = null!;
    public string Text { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Review()
    {
    }

    public Review(Guid courseId, Guid userId, int ratingValue, string text)
    {
        Id = Guid.NewGuid();
        CourseId = courseId;
        UserId = userId;
        Rating = Rating.Create(ratingValue);
        SetText(text);
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateReview(int ratingValue, string text)
    {
        Rating = Rating.Create(ratingValue);
        SetText(text);
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Review text is required.");
        }

        if (text.Length > 500)
        {
            throw new ArgumentException("Review text cannot exceed 500 characters.");
        }

        Text = text.Trim();
    }
}