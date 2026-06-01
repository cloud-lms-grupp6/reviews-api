using Reviews.Domain.Entities;

namespace Reviews.Application.Abstractions.Repositories;

public interface IReviewRepository
{
    Task<bool> ExistsForCourseAndUserAsync(Guid courseId, Guid userId, CancellationToken cancellationToken);
    Task AddAsync(Review review, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<Review?> GetByCourseAndUserAsync(Guid courseId, Guid userId, CancellationToken cancellationToken);
    Task DeleteAsync(Review review, CancellationToken cancellationToken);
    Task<List<Review>> GetByCourseIdAsync(Guid courseId, int skip, int take, CancellationToken cancellationToken);
    Task<int> CountByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
}