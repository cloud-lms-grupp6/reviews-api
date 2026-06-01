using Microsoft.AspNetCore.Mvc;
using Reviews.Api.Contracts;
using Reviews.Application.CreateReview;
using Reviews.Application.UpdateReview;
using Reviews.Application.DeleteReview;
using Reviews.Application.GetCourseReviews;
using Reviews.Application.GetRatingSummary;

namespace Reviews.Api.Controllers;

[ApiController]
[Route("api/courses/{courseId:guid}/reviews")]
public class ReviewsController(
    ICreateReviewService createReviewService, 
    IUpdateReviewService updateReviewService, 
    IDeleteReviewService deleteReviewService,
    IGetCourseReviewsService getCourseReviewsService,
    IGetRatingSummaryService getRatingSummaryService) : ControllerBase
{
    private readonly ICreateReviewService _createReviewService = createReviewService;
    private readonly IUpdateReviewService _updateReviewService = updateReviewService;
    private readonly IDeleteReviewService _deleteReviewService = deleteReviewService;
    private readonly IGetCourseReviewsService _getCourseReviewsService = getCourseReviewsService;
    private readonly IGetRatingSummaryService _getRatingSummaryService = getRatingSummaryService;

    [HttpPost]
    [EndpointSummary("Create a review for a course")]
    [EndpointDescription("Creates a review for the specified course. A user can only create one review per course.")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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
    [EndpointSummary("Update an existing review")]
    [EndpointDescription("Updates the rating and text of an existing review for the specified course.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [EndpointSummary("Delete a review")]
    [EndpointDescription("Deletes the user's review for the specified course.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [EndpointSummary("Get reviews for a course")]
    [EndpointDescription("Returns a paginated list of reviews for the specified course ordered by newest first.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    [HttpGet("rating-summary")]
    [EndpointSummary("Get course rating summary")]
    [EndpointDescription("Returns the average rating, rating distribution, percentages, and total review count for the specified course.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRatingSummary(Guid courseId, CancellationToken cancellationToken)
    {
        var result = await _getRatingSummaryService.GetSummaryAsync(courseId, cancellationToken);
        return Ok(result);
    }
}