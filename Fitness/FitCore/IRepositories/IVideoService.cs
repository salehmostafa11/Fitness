using FitCore.Dto.Admin;
using FitCore.Dto.NutritionistAndPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.IRepositories
{
    public interface IVideoService
    {
        Task<List<VideoView>> GetAllVideoAsync(string userId);
        Task<VideoView> GetByIdAsync(int id , string userId);
        Task<VideoView> AddVideo(AddAndEditVideo model, string UserID);
        Task<AddAndEditVideo> EditVideo(AddAndEditVideo model , int id , string userId);
        Task<bool> DeleteVideoAsync(int id , string userId);

    }
}
