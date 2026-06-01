using FluentAssertions;
using Moq;
using Reviews.Application.Abstractions.Repositories;
using Reviews.Application.CreateReview;

namespace Reviews.Tests.UnitTests;

public class CreateReviewServiceTests
{
    [Fact]
    public async Task CreateAsync_Should_Throw_When_User_Already_Reviewed_Course()
    {
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        reviewRepositoryMock
            .Setup(repository =>
                repository.ExistsForCourseAndUserAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var service = new CreateReviewService(reviewRepositoryMock.Object);

        var act = async () => await service.CreateAsync(
            Guid.NewGuid(),
            Guid.NewGuid(),
            5,
            "Great course!",
            CancellationToken.None);

        await act.Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage("User has already reviewed this course.");
    }
}