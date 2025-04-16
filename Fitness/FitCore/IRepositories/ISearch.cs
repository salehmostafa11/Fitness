using FitCore.Dto.Search;
using FitCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.IRepositories
{
    public interface ISearch
    {
        Task<List<GetTrainee>> GetUsersByRoleAsync(string role);
        Task<List<GetTrainee>> GetAllTrainees();
        Task<List<GetNutritionistsAndTrainers>> GetAllTrainers();
        Task<List<GetNutritionistsAndTrainers>> GetAllNutritionist();


        Task<GetTrainee> SearchUserById(string id);
        Task<List<GetTrainee>> SearchUserByName(string name);
        List<GetTrainee> MapUsersToUser(List<ApplicationUser> users);
    }
}
