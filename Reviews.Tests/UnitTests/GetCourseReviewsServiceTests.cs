using FluentAssertions;
using Moq;
using Reviews.Application.Abstractions.Repositories;
using Reviews.Application.GetCourseReviews;
using Reviews.Domain.Entities;

namespace Reviews.Tests.UnitTests;

public class GetCourseReviewsServiceTests
{
    [Fact]
    public async Task GetAsync_Should_Return_Paginated_Reviews_With_Metadata()
    {
        var courseId = Guid.NewGuid();

        var reviews = new List<Review>
        {
            new Review(courseId, Guid.NewGuid(), 5, "Great course!"),
            new Review(courseId, Guid.NewGuid(), 3, "Okay course.")
        };

        var reviewRepositoryMock = new Mock<IReviewRepository>();

        reviewRepositoryMock
            .Setup(repository =>
                repository.GetByCourseIdAsync(
                    courseId,
                    2,
                    2,
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(reviews);

        reviewRepositoryMock
            .Setup(repository =>
                repository.CountByCourseIdAsync(
                    courseId,
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(5);

        var service = new GetCourseReviewsService(
            reviewRepositoryMock.Object);

        var result = await service.GetAsync(
            courseId,
            pageNumber: 2,
            pageSize: 2,
            CancellationToken.None);

        result.PageNumber.Should().Be(2);
        result.PageSize.Should().Be(2);
        result.TotalCount.Should().Be(5);

        result.TotalPages.Should().Be(3);

        result.Items.Should().HaveCount(2);
        result.Items[0].Rating.Should().Be(5);
        result.Items[0].Text.Should().Be("Great course!");
        result.Items[1].Rating.Should().Be(3);
        result.Items[1].Text.Should().Be("Okay course.");
    }
}