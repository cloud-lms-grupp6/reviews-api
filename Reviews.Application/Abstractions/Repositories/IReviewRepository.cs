using Reviews.Domain.Entities;

namespace Reviews.Application.Abstractions.Repositories;

public interface IReviewRepository
{
    Task<bool> ExistsForCourseAndUserAsync(Guid courseId, Guid userId, CancellationToken cancellationToken);
    Task AddAsync(Review review, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}