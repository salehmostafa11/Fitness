using FitCore.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Fit.Controllers
{
    public class SearchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SearchController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Get-All-Trainees")]
        public async Task<IActionResult> GetAllTrainees()
        {
            var result = await _unitOfWork.Search.GetAllTrainees();

            if (!result.Any())
                return Ok("No Trainees Found");

            return Ok(result);
        }

        [HttpGet("Get-All-Trainers")]
        public async Task<IActionResult> GetAllTrainers()
        {
            var result = await _unitOfWork.Search.GetAllTrainers();

            if (result == null || !result.Any())
                return NotFound("No Trainers Found");


            return Ok(result);
        }
        [HttpGet("Get-All-Nutritionist")]
        public async Task<IActionResult> GetAllNutritionist()
        {
            var result = await _unitOfWork.Search.GetAllNutritionist();

            if (result == null || !result.Any())
                return NotFound("No Nutritionists Found");


            return Ok(result);
        }

        [HttpGet("Get-By-Name")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required.");

            var result = await _unitOfWork.Search.SearchUserByName(name);

            if (result is null || !result.Any())
                return NotFound($"User not found with name {name}");

            return Ok(result);
        }

        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _unitOfWork.Search.SearchUserById(id);

            if (result is null)
                return NotFound($"User not found with ID = {id}");

            return Ok(result);
        }
    }
}
