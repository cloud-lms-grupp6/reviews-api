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

    public async Task<Review?> GetByCourseAndUserAsync(Guid courseId, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews
            .FirstOrDefaultAsync(review => review.CourseId == courseId && review.UserId == userId, cancellationToken);
    }

    public Task DeleteAsync(Review review, CancellationToken cancellationToken)
    {
        _dbContext.Reviews.Remove(review);

        return Task.CompletedTask;
    }

    public async Task<List<Review>> GetByCourseIdAsync(Guid courseId, int skip, int take, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews
            .Where(review => review.CourseId == courseId)
            .OrderByDescending(review => review.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountByCourseIdAsync(Guid courseId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews.CountAsync(review => review.CourseId == courseId, cancellationToken);
    }

    public async Task<List<int>> GetRatingsByCourseIdAsync(Guid courseId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews
            .Where(review => review.CourseId == courseId)
            .Select(review => review.Rating.Value)
            .ToListAsync(cancellationToken);
    }
}