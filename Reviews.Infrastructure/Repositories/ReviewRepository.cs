using Microsoft.EntityFrameworkCore;
using Reviews.Application.Abstractions.Repositories;
using Reviews.Domain.Entities;
using Reviews.Infrastructure.Persistence;

namespace Reviews.Infrastructure.Repositories;

public class ReviewRepository(ReviewsDbContext dbContext) : IReviewRepository
{
    private readonly ReviewsDbContext _dbContext = dbContext;

    public async Task<bool> ExistsForCourseAndUserAsync(Guid courseId, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews.AnyAsync(
            review => review.CourseId == courseId &&
                      review.UserId == userId,
                      cancellationToken);
    }

    public async Task AddAsync(Review review, CancellationToken cancellationToken)
    {
        await _dbContext.Reviews.AddAsync(review, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}