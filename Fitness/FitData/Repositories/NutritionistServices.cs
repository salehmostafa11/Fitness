using FitCore.Dto.Authentication;
using FitCore.Dto.NutritionistAndPlan;
using FitCore.IRepositories;
using FitCore.Models;
using FitData.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitData.Repositories
{
    public class NutritionistServices : INutritionistServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public NutritionistServices(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<EditPlans> EditPlanAsync(string UserId, EditPlans model, int id)
        {
            var oldData = await _context.nutritionPlans
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted && b.NutritionistId == UserId);
            if (oldData is null) return null;

            oldData.Name = model.Name;
            oldData.Details = model.Details;
            oldData.MealTypeId = (model.MealTypeId > 3 || model.MealTypeId < 1) ? 4 : model.MealTypeId;

            await _context.SaveChangesAsync();

            return new EditPlans
                    {
                        Name = oldData.Name,
                        Details = oldData.Details,
                        MealTypeId = oldData.MealTypeId
                    };
        }
        public async Task<List<ViewPlans>> GetMyPlans(string id)
        {
            var result = await _context.nutritionPlans.Where(b=>b.NutritionistId == id && !b.IsDeleted  )
                .Select(plan => new ViewPlans
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    Details = plan.Details,
                    TraineeId = plan.TraineeId,
                    MealType = (plan.MealTypeId > 3 || plan.MealTypeId < 1) ? 4 : plan.MealTypeId,
                    TraineeName = plan.Trainee != null ? plan.Trainee.FirstName + " " + plan.Trainee.LastName : "",
                    NutritionistId = plan.NutritionistId,
                    NutritionistName = plan.Nutritionist != null ? plan.Nutritionist.FirstName + " " + plan.Nutritionist.LastName : "",
                })
                .ToListAsync();
            return result;
        }
        public async Task<ViewPlansById> GetPlanByIdAsync(string UserId, int id)
        {
            var plan = await _context.nutritionPlans
                 .Include(p => p.Trainee)
                 .Include(p=>p.Nutritionist)
                 .Where(b=> !b.IsDeleted)
                 .FirstOrDefaultAsync(p => p.Id == id && p.NutritionistId == UserId);

            if (plan == null) return null;

            return new ViewPlansById
            {
                Id = plan.Id,
                Name = plan.Name,
                Details = plan.Details,
                MealTypeId = plan.MealTypeId,
                TraineeId = plan.TraineeId,
                NutritionistId = UserId,
                TraineeName = plan.Trainee != null ? plan.Trainee.FirstName + " " + plan.Trainee.LastName : "",
                NutritionistName = plan.Nutritionist != null ? plan.Nutritionist.FirstName + " " + plan.Nutritionist.LastName : "",
            };
        }
        public async Task<bool> DeleteAsync(string UserId, int id)
        {
            var plan = await _context.nutritionPlans.FirstOrDefaultAsync(b=>b.Id == id && b.NutritionistId == UserId);

            if (plan is null)
                return false;
            plan.IsDeleted = true;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<AddPlan> CreatePlanAsync(AddPlan model, string UserId)
        {
            var plan = new NutritionPlans
            {
                Name = model.Name,
                Details = model.Details,
                NutritionistId = UserId,
                MealTypeId = (model.MealTypeId > 3 || model.MealTypeId < 1) ? 4 : model.MealTypeId,
                TraineeId = model.TraineeId,
                IsDeleted = false
            };

            var result = await _userManager.FindByIdAsync(model.TraineeId);
            var isTrainee = await _userManager.IsInRoleAsync(result,ApplicationRoles.TraineesRole);
            if (result is null || !isTrainee)
            {
                return null;
            }
            _context.nutritionPlans.Add(plan);
            _context.SaveChanges();
            return model;
        }
        public async Task<List<ViewPlans>> ViewAllPlans()
        {
            var Data = await _context.nutritionPlans
                .Select(b => new ViewPlans
                {
                    Id = b.Id,
                    Name = b.Name,
                    Details = b.Details,
                    NutritionistId = b.NutritionistId,
                    NutritionistName = b.Nutritionist != null ? b.Nutritionist.FirstName + " " + b.Nutritionist.LastName : "",
                    TraineeId = b.TraineeId,
                    TraineeName = b.Trainee != null ? b.Trainee.FirstName + " " + b.Trainee.LastName : ""
                }).AsNoTracking().ToListAsync();
            return Data;
        }
    }
}
