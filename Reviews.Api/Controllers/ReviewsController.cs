using Microsoft.AspNetCore.Mvc;
using Reviews.Api.Contracts;
using Reviews.Application.CreateReview;

namespace Reviews.Api.Controllers;

[ApiController]
[Route("api/courses/{courseId:guid}/reviews")]
public class ReviewsController(ICreateReviewService createReviewService) : ControllerBase
{
    private readonly ICreateReviewService _createReviewService = createReviewService;

    [HttpPost]
    public async Task<IActionResult> CreateReview(Guid courseId, CreateReviewRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var review = await _createReviewService.CreateAsync(courseId, request.UserId, request.Rating, request.Text, cancellationToken);

            return Created($"/api/courses/{courseId}/reviews/{review.Id}",
                new
                {
                    review.Id,
                    review.CourseId,
                    review.UserId,
                    Rating = review.Rating.Value,
                    review.Text,
                    review.CreatedAt
                });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(exception.Message);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}