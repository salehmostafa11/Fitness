using Fit.Authorization;
using FitCore.Dto.Admin;
using FitCore.IRepositories;
using FitCore.Dto.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fit.Controllers
{
    [Route("api/[controller]")]
    [TrainerAuthorize]
    public class VideoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public VideoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Get-My-Videos")]
        public async Task<IActionResult> GetAllVideos()
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.VideoService.GetAllVideoAsync(userId);

            if(result is null || !result.Any()) 
                return NotFound($"No videos found");

            return Ok(result);
        }

        [HttpGet("Get-Video-By-Id")]
        public async Task<IActionResult> GetVideoById(int id)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.VideoService.GetByIdAsync(id , userId);

            if (result is null)
                return NotFound($"No videos found With {id}");

            return Ok(result);
        }
        [HttpPost("Add-Video")]
        public async Task<IActionResult> AddVideo([FromForm] AddAndEditVideo model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.VideoService.AddVideo(model , userId);

            if (result == null)
                return BadRequest("Failed to add video.");


            return Ok(result);
        }

        [HttpPut("Edit-Video/{id}")]
        public async Task<IActionResult> EditVideo([FromForm] AddAndEditVideo model , int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.VideoService.EditVideo(model , id , userId);

            if (result is null)
                return NotFound($"No video found with ID = {id}");


            return Ok(result);
        }

        [HttpDelete("Delete-Video/{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.VideoService.DeleteVideoAsync(id, userId);

            if (!result)
                return NotFound($"Video with ID {id} not found.");

            return Ok("Video deleted successfully.");
        }



    }
}
