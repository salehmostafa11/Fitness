using FitCore.Dto.Authentication;
using FitCore.IRepositories;
using FitData.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Fit.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Register-Admin")]
        public async Task<IActionResult> RegisterAdminAsync([FromForm] AdminModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.AuthServices.RegisterAdmin(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new
            {
                expiresOn = result.ExpireOn,
                Role = result.Roles,
                token = result.Token,
            });
        }

        [HttpPost("Register-Trainee")]
        public async Task<IActionResult> RegisterTraineeAsync([FromForm] RegisterTraineeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.AuthServices.RegisterAsyncTrainee(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(
            new
            {
                expiresOn = result.ExpireOn,
                Role = result.Roles,
                token = result.Token,
            });
        }


        [HttpPost("Register-Trainer")]
        public async Task<IActionResult> RegisterTrainerAsync([FromForm] RegisterTrainerAndNutritionistModel model)
        {
            return await RegisterGenericAsync(model, ApplicationRoles.TrainersRole);

        }
        [HttpPost("Register-Nutritionist")]
        public async Task<IActionResult> RegisterNutritionistAsync([FromForm] RegisterTrainerAndNutritionistModel model)
        {
            return await RegisterGenericAsync(model, ApplicationRoles.NutritionistsRole);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GetTokenRequest([FromForm] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.AuthServices.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(
            new
            {
                email = result.Email,
                expiresOn = result.ExpireOn,
                Role = result.Roles,
                token = result.Token,
            });
        }





        private async Task<IActionResult> RegisterGenericAsync(RegisterTrainerAndNutritionistModel model, string role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.AuthServices.RegisterAsyncTrainer(model, role);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new
            {
                expiresOn = result.ExpireOn,
                Role = result.Roles,
                token = result.Token,
            });
        }

    }
}
