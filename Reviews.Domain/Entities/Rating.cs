namespace Reviews.Domain.Entities;


public sealed class Rating
{
    public int Value { get; }

    private Rating(int value)
    {
        Value = value;
    }

    public static Rating Create(int value)
    {
        if (value < 1 || value > 5)
        {
            throw new ArgumentException("Rating must be between 1 and 5 stars.");
        }

        return new Rating(value);
    }
}