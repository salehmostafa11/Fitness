using Fit.Authorization;
using FitCore.Dto.NutritionistAndPlan;
using FitCore.IRepositories;
using FitCore.Dto.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [NutritionistAuthorizeAttribute]
    public class NutritionistController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public NutritionistController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("Create-Plan")]
        public async Task<IActionResult> CreatePlan([FromForm] AddPlan model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.FindFirstValue("uid");
            var result = await _unitOfWork.NutritionistServices.CreatePlanAsync(model, userId);
            if (result == null)
                return BadRequest("Failed to create plan.");
            return Ok(result);
        
        }

        [HttpGet("Get-My-Plans")]
        public async Task<IActionResult> GetPlans()
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.NutritionistServices.GetMyPlans(userId);

            if (result is null)
                return BadRequest("No Plans Find");

            return Ok(result);
        }
        
        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> GetPlansById(int id)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.NutritionistServices.GetPlanByIdAsync(userId , id);

            if (result is null)
                return BadRequest("No Plans Find");

            return Ok(result);

        }

        [HttpPut("Edit-Plan")]
        public async Task<IActionResult> EditPlan([FromForm] EditPlans model ,int id)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.NutritionistServices.EditPlanAsync(userId , model, id);
            if (result == null)
                return NotFound($"No plan found with ID: {id}");

            return Ok(result);
        }

        [HttpPut("Delete-Plan")]
        public async Task<IActionResult> DeletePlan([FromForm]int id)
        {
            var userId = User.FindFirstValue("uid");

            var result = await _unitOfWork.NutritionistServices.DeleteAsync(userId , id);
            if (!result)
                return NotFound($"No plan found with ID: {id}");

            return Ok($"Plan With ID: {id} Deleted");

        }


        [HttpGet("Get-All-Plans-Admin")]
        [AdminAuthorize]
        public async Task<IActionResult> GetAllUserPlans()
        {
            var result = await _unitOfWork.NutritionistServices.ViewAllPlans();

            if (result is null || !result.Any())
                return BadRequest("No Plans Find");

            return Ok(result);
        }
    }
}
