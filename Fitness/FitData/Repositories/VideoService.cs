using FitCore.Dto.Admin;
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
    public class VideoService : IVideoService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public VideoService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<VideoView>> GetAllVideoAsync(string userId)
        {
            var data = await _context.Video.Where(i => i.TrainnerId == userId)
               .Select(b => new VideoView
               {
                   Id = b.Id,
                   Tilte = b.Tilte,
                   VideoLink = b.VideoLink,
                   Level = b.LevelId,
                   ExerciseTypeId = b.ExerciseTypeId,

               }).AsNoTracking().ToListAsync();

            if (!data.Any()) return data;

            return data;
        }

        public async Task<VideoView> AddVideo(AddAndEditVideo model , string UserID)
        {
            var newVideo = new Video
            {
                Tilte = model.Tilte,
                VideoLink = model.VideoLink,
                LevelId = (model.Level > 3 || model.Level < 1) ? 1 : model.Level,
                TrainnerId = UserID,
                ExerciseTypeId = (model.ExerciseTypeId < 9 || model.ExerciseTypeId < 1) ? 9 : model.ExerciseTypeId
            };
            await _context.Video.AddAsync(newVideo);

            await _context.SaveChangesAsync();

            return new VideoView
            {
                Id = newVideo.Id,
                Tilte = newVideo.Tilte,
                VideoLink = newVideo.VideoLink,
                Level = newVideo.LevelId,
                ExerciseTypeId = model.ExerciseTypeId,
            };

        }

        public async Task<VideoView> GetByIdAsync(int id , string userId)
        {
            var result = await _context.Video.Where(i => i.TrainnerId == userId)
                .Select(b => new VideoView
                {
                    Id = b.Id,
                    Tilte = b.Tilte,
                    VideoLink = b.VideoLink,
                    Level = b.LevelId,
                    ExerciseTypeId = b.ExerciseTypeId

                }).AsNoTracking().FirstOrDefaultAsync(b=>b.Id == id);

            return result;

        }

        public async Task<AddAndEditVideo> EditVideo(AddAndEditVideo model , int id , string userId)
        {
            var oldData = await _context.Video.FirstOrDefaultAsync(i => i.TrainnerId == userId && i.Id == id);

            if (oldData is null)
                return null;

            oldData.LevelId = (model.Level > 3 || model.Level < 1) ? 1 : model.Level;
            oldData.Tilte = model.Tilte;
            oldData.VideoLink = model.VideoLink;
            oldData.ExerciseTypeId = ( model.ExerciseTypeId < 9 || model.ExerciseTypeId < 1) ? 9 : model.ExerciseTypeId ;

            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteVideoAsync(int id, string userId)
        {
            var video = await _context.Video.FirstOrDefaultAsync(i => i.TrainnerId == userId && i.Id == id);

            if (video is null)
                return false;

            _context.Video.Remove(video);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
