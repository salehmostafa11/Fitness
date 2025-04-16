using FitCore.Dto.Authentication;
using FitCore.Dto.Search;
using FitCore.IRepositories;
using FitCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitData.Repositories
{
    public class Search : ISearch
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Search(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public List<GetTrainee> MapUsersToUser(List<ApplicationUser> users)
        {
            return users.Select(user => new GetTrainee
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Height = user.Height,
                Weight = user.Weight,
                Gender = user.Gender,
                Level = user.LevelId
            }).ToList();
        }
        public List<GetNutritionistsAndTrainers> MapUsersToTrainerAndNutritionist(List<ApplicationUser> users)
        {
            return users.Select(user => new GetNutritionistsAndTrainers
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ExYreas = user.ExperienceYears
            }).ToList();
        }
        // Return null because the user does not exist
        public async Task<GetTrainee> SearchUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            var SearchUserById = await _userManager.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (SearchUserById is null)
                return null;

            return MapUsersToUser(new List<ApplicationUser> { SearchUserById }).FirstOrDefault();
        }
        public async Task<List<GetTrainee>> SearchUserByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<GetTrainee>();

            var users = await _userManager.Users
                .AsNoTracking()
                .Where(u => EF.Functions.Like(u.FirstName, $"{name}%") ||
                            EF.Functions.Like(u.LastName, $"{name}%"))
                .ToListAsync();

            var filteredTrainees = new List<ApplicationUser>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, ApplicationRoles.TraineesRole))
                    filteredTrainees.Add(user);
            }

            if(!filteredTrainees.Any())
                return new List<GetTrainee>();

            return MapUsersToUser(filteredTrainees);
        }
        public async Task<List<GetTrainee>> GetUsersByRoleAsync(string role)
        {
            if (string.IsNullOrEmpty(role))
                return new List<GetTrainee>();

            var users = await _userManager.GetUsersInRoleAsync(role);
            if (users is null || !users.Any())
                return new List<GetTrainee>();

            return MapUsersToUser(users.ToList());
        }
        public async Task<List<GetNutritionistsAndTrainers>> GetTrainersAndNutritionistByRoleAsync(string role)
        {
            if (string.IsNullOrEmpty(role))
                return new List<GetNutritionistsAndTrainers>();

            var users = await _userManager.GetUsersInRoleAsync(role);
            if (users is null || !users.Any())
                return new List<GetNutritionistsAndTrainers>();

            return MapUsersToTrainerAndNutritionist(users.ToList());
        }
        public Task<List<GetTrainee>> GetAllTrainees()
        {
            return GetUsersByRoleAsync(ApplicationRoles.TraineesRole);
        }
        public Task<List<GetNutritionistsAndTrainers>> GetAllTrainers()
        {
            return GetTrainersAndNutritionistByRoleAsync(ApplicationRoles.TrainersRole);
        }
        public Task<List<GetNutritionistsAndTrainers>> GetAllNutritionist()
        {
            return GetTrainersAndNutritionistByRoleAsync(ApplicationRoles.NutritionistsRole);
        }
    }
}
