using FitCore.Dto.NutritionistAndPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.IRepositories
{
    public interface INutritionistServices
    {
        Task<List<ViewPlans>> GetMyPlans(string id);
        Task<EditPlans> EditPlanAsync(string UserId, EditPlans model , int id);
        Task<AddPlan> CreatePlanAsync(AddPlan model , string UserId);
        Task<ViewPlansById> GetPlanByIdAsync(string UserId, int id);
        Task<bool> DeleteAsync(string UserId, int id);
        Task<List<ViewPlans>> ViewAllPlans();

    }
}
