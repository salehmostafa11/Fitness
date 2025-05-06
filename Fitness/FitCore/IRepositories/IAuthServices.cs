using FitCore.Dto.Authentication;
using FitCore.Dto.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.IRepositories
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterAsyncTrainee(RegisterTraineeModel model);
        Task<AuthModel> RegisterAdmin(AdminModel model);
        Task<AuthModel> RegisterAsyncTrainer(RegisterTrainerAndNutritionistModel model , string role);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordModel model);
        Task<bool> UpdateUserProfileAsync(string userId, UpdateUserProfileModel model);
        Task<UserProfile> GetDetailes(string userId);

        Task<PasswordActionResult> ForgotPasswordAsync(ForgotPasswordModel model);
        Task<PasswordActionResult> ResetPasswordAsync(ResetPasswordModel model);
    }
}
