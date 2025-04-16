using Fit.Authorization;
using FitCore.Dto.VideoReview;
using FitCore.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        [TraineeAuthorize]
        public class VideoReviewController : ControllerBase
        {
        private readonly IUnitOfWork _unitOfWork;
        public VideoReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Get-My-Videos-Reviews")]
        public async Task<IActionResult> GetMyVideosReviews()
        {
            var userId = User.FindFirstValue("uid");
            var result = await _unitOfWork.VideoReviewServices.GetAllMyVideoReview(userId);

            if (result is null || !result.Any())
                return NotFound("No reviews found for this user.");

            return Ok(new
            {
                message = "Reviews :",
                Result = result
            });
        }

        [HttpGet("Get-Video-Reviews-By-Id-Login")]
        public async Task<IActionResult> GetVideoReviewsByIdWithLogin(int ReviewId )
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.VideoReviewServices.GetVideoReviewById(ReviewId , userId);
            if (result == null)
                return NotFound($"No reviews found With this id = {ReviewId}.");
            return Ok(new
            {
                message = "Review :",
                Result = result
            });
        }

        // You Can Use it Without Login
        [HttpGet("Get-Video-Reviews-By-Id-Wihout-Login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVideoReviewsById(int ReviewId)
        {
            var result = await _unitOfWork.VideoReviewServices.GetVideoReviewById(ReviewId);
            if (result == null)
                return NotFound($"No reviews found With this id = {ReviewId}.");
            return Ok(new
            {
                message = "Review :",
                Result = result
            });
        }

        [HttpPost("Add-Video-Review")]
        public async Task<IActionResult> AddVideoReview([FromForm]NewVideoReview model)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.VideoReviewServices.AddVideoReview(model, userId);

            if(result == null)
                return BadRequest("You have already reviewed this video or the video does not exist.");

            return Ok(new
            {
                message = "Review Created successfully.",
                Result = result
            });
        }

        [HttpPut("Edit-Video-Review")]
        public async Task<IActionResult> EditVideoReview([FromForm] EditVideoReview model, int videoId)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.VideoReviewServices.EditVideoReview(model, userId ,videoId);

            if (result == null)
                return NotFound("No existing review found for this video to update.");

            return Ok(new
            {
                message = "Review updated successfully.",
                Result = result
            });
        }

        [HttpDelete("Delete-Video-Review")]
        public async Task<IActionResult> DeleteVideoReview(int videoId)
        {
            var userId = User.FindFirstValue("uid");
            var result = await _unitOfWork.VideoReviewServices.DeleteVideoReview(userId, videoId);
            if (result == null)
                return NotFound("No existing review found for this video to delete.");
            return Ok(new
            {
                message = "Review deleted successfully.",
                Result = result
            });
        }
    }
}
