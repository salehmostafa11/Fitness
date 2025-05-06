using FitCore.IRepositories;
using FitCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using FitCore.Dto.Authentication;
using FitCore.Dto.Profile;

namespace FitData.Repositories
{
    public class AuthServices : IAuthServices
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSetting _jwt;
        public AuthServices(UserManager<ApplicationUser> userManager, IOptions<JwtSetting> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }
        public async Task<AuthModel> RegisterAdmin(AdminModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };

            return await RegisterUser(user, model.Password, ApplicationRoles.AdminRole);
        }
        private async Task<AuthModel> RegisterUser(ApplicationUser user, string password, string role)
        {
            if (await _userManager.FindByEmailAsync(user.Email) is not null)
                return new AuthModel { Message = "Email is already in use." };

            if (await _userManager.FindByNameAsync(user.UserName) is not null)
                return new AuthModel { Message = "Username is already taken." };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errorList = string.Join("\n", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errorList };
            }

            await _userManager.AddToRoleAsync(user, role);

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                UserName = user.UserName,
                Email = user.Email,
                IsAuthenticated = true,
                Roles = new List<string> { role },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpireOn = jwtSecurityToken.ValidTo
            };
        }
        public async Task<AuthModel> RegisterAsyncTrainee(RegisterTraineeModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                Height = model.Height,
                Weight = model.Weight,
                Age = model.Age,
                Gender = model.Gender,
                LevelId = model.Level == 0 ? 1 : model.Level
            };

            return await RegisterUser(user, model.Password, ApplicationRoles.TraineesRole);
        }
        public async Task<AuthModel> RegisterAsyncTrainer(RegisterTrainerAndNutritionistModel model , string role)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                ExperienceYears = model.ExperienceYears
            };

            return await RegisterUser(user, model.Password, role);
        }
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email Or Password is Incorrect";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var roleList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = model.Email;
            //authModel.ExpireOn = jwtSecurityToken.ValidTo;
            authModel.Roles = roleList.ToList();


            return authModel;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            // This line is calling a method to get the claims for the given user
            var userClaim = await _userManager.GetClaimsAsync(user);
            var userRole = await _userManager.GetRolesAsync(user);

            // Claims in the context of authentication are pieces of information about the user
            var roleClaim = new List<Claim>();

            foreach (var role in userRole)
            {
                roleClaim.Add(new Claim("role", role));
            }
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),  
            // Sub: The subject of the token, usually the username or user identifier
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
            // Jti: JWT ID (unique identifier for the token)
            new Claim(JwtRegisteredClaimNames.Email, user.Email),  
            // Email: The user's email address
            new Claim("uid", user.Id)  
            // Custom claim "uid" to store the user's ID
        }
            .Union(userClaim)
            .Union(roleClaim);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            // SigningCredentials is used to define how the token will be signed to ensure its integrity and authenticity

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials
                );


            return jwtSecurityToken;
        }
        public async Task<UserProfile> GetDetailes(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId); 

            if (user != null)
            {
                UserProfile userProfile = new UserProfile
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age,
                    Height = user.Height,
                    Weight = user.Weight,
                    Gender = user.Gender,
                    Level = user.LevelId,

                };
                return userProfile;
            }
            else
            {
                return null;
            }

            
        }
        public async Task<bool> UpdateUserProfileAsync(string userId, UpdateUserProfileModel model)
        {
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null) return false;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Height = model.Height;
            user.Weight = model.Weight;
            user.Gender = (int)model.Gender;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordModel model)
        {
            if (model.NewPassword != model.ConfirmNewPassword)
                return false;

            var user = await _userManager.FindByNameAsync(userId);
            if (user == null) return false;

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            return result.Succeeded;
        }

        public Task<PasswordActionResult> ForgotPasswordAsync(ForgotPasswordModel model)
        {
            throw new NotImplementedException();
        }

        public Task<PasswordActionResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            throw new NotImplementedException();
        }
    }
}
