using FluentAssertions;
using Moq;
using Reviews.Application.Abstractions.Repositories;
using Reviews.Application.GetRatingSummary;

namespace Reviews.Tests.UnitTests;

public class GetRatingSummaryServiceTests
{
    [Fact]
    public async Task GetSummaryAsync_Should_Calculate_Average_And_Distribution()
    {
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        reviewRepositoryMock.Setup(repository => repository.GetRatingsByCourseIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([5, 3, 2, 1]);

        var service = new GetRatingSummaryService(reviewRepositoryMock.Object);

        var result = await service.GetSummaryAsync(
            Guid.NewGuid(),
            CancellationToken.None);

        result.AverageRating.Should().Be(2.75);

        result.TotalReviews.Should().Be(4);

        result.OneStarCount.Should().Be(1);
        result.TwoStarCount.Should().Be(1);
        result.ThreeStarCount.Should().Be(1);
        result.FourStarCount.Should().Be(0);
        result.FiveStarCount.Should().Be(1);

        result.OneStarPercentage.Should().Be(25);
        result.TwoStarPercentage.Should().Be(25);
        result.ThreeStarPercentage.Should().Be(25);
        result.FourStarPercentage.Should().Be(0);
        result.FiveStarPercentage.Should().Be(25);
    }

    [Fact]
    public async Task GetSummaryAsync_Should_Return_Empty_Summary_When_No_Reviews_Exist()
    {
        var reviewRepositoryMock = new Mock<IReviewRepository>();

        reviewRepositoryMock
            .Setup(repository =>
                repository.GetRatingsByCourseIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var service = new GetRatingSummaryService(reviewRepositoryMock.Object);

        var result = await service.GetSummaryAsync(Guid.NewGuid(), CancellationToken.None);

        result.TotalReviews.Should().Be(0);
        result.AverageRating.Should().Be(0);
    }
}