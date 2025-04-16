using FitCore.Dto.Profile;
using FitCore.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fit.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("User-Profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirstValue("uid");
            if (userId == null) return Unauthorized();

            var result = await _unitOfWork.AuthServices.GetDetailes(userId);
            if (result == null) return NotFound("User not found");


            return Ok(result);

        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateUserProfileModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var result = await _unitOfWork.AuthServices.UpdateUserProfileAsync(userId, model);

            return result ? Ok("Profile updated successfully.") : BadRequest("Failed to update profile.");
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordModel model)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var result = await _unitOfWork.AuthServices.ChangePasswordAsync(userId, model);

            return result ? Ok("Password changed successfully.") : BadRequest("Failed to change password.");
        }


    }
}
