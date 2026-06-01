using Microsoft.AspNetCore.Mvc;
using Reviews.Api.Contracts;
using Reviews.Application.CreateReview;
using Reviews.Application.UpdateReview;
using Reviews.Application.DeleteReview;
using Reviews.Application.GetCourseReviews;

namespace Reviews.Api.Controllers;

[ApiController]
[Route("api/courses/{courseId:guid}/reviews")]
public class ReviewsController(
    ICreateReviewService createReviewService, 
    IUpdateReviewService updateReviewService, 
    IDeleteReviewService deleteReviewService,
    IGetCourseReviewsService getCourseReviewsService) : ControllerBase
{
    private readonly ICreateReviewService _createReviewService = createReviewService;
    private readonly IUpdateReviewService _updateReviewService = updateReviewService;
    private readonly IDeleteReviewService _deleteReviewService = deleteReviewService;
    private readonly IGetCourseReviewsService _getCourseReviewsService = getCourseReviewsService;

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

    [HttpPut]
    public async Task<IActionResult> UpdateReview(Guid courseId, UpdateReviewRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _updateReviewService.UpdateAsync(courseId, request.UserId, request.Rating, request.Text, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException exception)
        {
            return NotFound(exception.Message);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete]
    // [FromQuery] to be replaced when JWT is implemented
    public async Task<IActionResult> DeleteReview([FromQuery] Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        try
        {
            await _deleteReviewService.DeleteAsync(courseId, userId, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCourseReviews(Guid courseId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _getCourseReviewsService.GetAsync(courseId, pageNumber, pageSize, cancellationToken);
            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}