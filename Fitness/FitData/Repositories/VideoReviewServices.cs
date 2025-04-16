using FitCore.Dto.VideoReview;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitData.Repositories
{
    public class VideoReviewServices : IVideoReviewServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public VideoReviewServices(UserManager<ApplicationUser> userManager , ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<VideoReviewResponse> AddVideoReview(NewVideoReview model,string userId)
        {

            var VideoReviewIsExist = await _context.VideoReview.AsNoTracking()
                .AnyAsync(b=>b.VideoId == model.VideoId && b.TraineeId == userId);

            var VideoIsExist = await _context.Video.AsNoTracking()
                .AnyAsync(b => b.Id == model.VideoId);

            if (!VideoIsExist || VideoReviewIsExist)
                return null;
            
            var newVideoReview = new VideoReview();
            newVideoReview.TraineeId = userId;
            newVideoReview.VideoId = model.VideoId;
            newVideoReview.Review = model.Review == 0 ? 1 : model.Review;
            newVideoReview.Description = model.Description;
            
            await _context.VideoReview.AddAsync(newVideoReview);

            await _context.SaveChangesAsync();

            return new VideoReviewResponse
            {
                Id = newVideoReview.Id,
                Review = newVideoReview.Review,
                Description = newVideoReview.Description,
                VideoId = newVideoReview.VideoId,
                TraineeId = newVideoReview.TraineeId
            };
        }

        public async Task<VideoReviewResponse> EditVideoReview(EditVideoReview model, string userId , int videoId)
        {

            var oldData = await _context.VideoReview
                .FirstOrDefaultAsync(b => b.VideoId == videoId && b.TraineeId == userId);

            if (oldData is null)
                return null;

            oldData.Review = model.Review == 0 ? 1 : model.Review;
            oldData.Description = model.Description;


            await _context.SaveChangesAsync();

            return new VideoReviewResponse
            {
                Id = oldData.Id,
                Review = oldData.Review,
                Description = oldData.Description,
                VideoId = oldData.VideoId,
                TraineeId = oldData.TraineeId
            };
        }


        public async Task<VideoReviewResponse> DeleteVideoReview(string userId, int videoId)
        {
            var Data = _context.VideoReview
                .FirstOrDefault(b => b.VideoId == videoId && b.TraineeId == userId);
            if(Data is null)
                return null;

            _context.VideoReview.Remove(Data);
            await _context.SaveChangesAsync();

            return new VideoReviewResponse
            {
                Id = Data.Id,
                Review = Data.Review,
                Description = Data.Description,
                VideoId = Data.VideoId,
                TraineeId = Data.TraineeId
            };
        }
        public async Task<VideoReviewResponseFullData> GetVideoReviewById(int Reviewid)
        {
            var Data = _context.VideoReview.AsNoTracking()
                .Include(r => r.Trainee)
                .Include(r => r.Video)
                .AsSplitQuery()
                .FirstOrDefault(b => b.Id == Reviewid);

            if (Data is null)
                return null;

            return new VideoReviewResponseFullData
            {
                Id = Data.Id,
                Review = Data.Review,
                Description = Data.Description,
                VideoId = Data.VideoId,
                VideoTitle = Data.Video.Tilte,
                TraineeId = Data.TraineeId,
                TraineeName = Data.Trainee.FirstName + " " + Data.Trainee.LastName
            };
        }
        public async Task<List<VideoReviewResponseFullData>> GetAllMyVideoReview(string userId)
        {
            var Data =await _context.VideoReview.AsNoTracking()
                .Include(r => r.Video)
                .AsSplitQuery()
                .Where(b => b.TraineeId == userId)
                .Select(b =>  new VideoReviewResponseFullData
                {
                    Id = b.Id,
                    Review = b.Review,
                    Description = b.Description,
                    VideoId = b.VideoId,
                    VideoTitle = b.Video.Tilte,
                    TraineeId = b.TraineeId,
                    TraineeName = b.Trainee.FirstName + " " + b.Trainee.LastName
                })
                .ToListAsync();

            if (Data is null)
                return null;

            return Data;
        }

        public async Task<VideoReviewResponseFullData> GetVideoReviewById(int Reviewid, string userId)
        {
            var Data = _context.VideoReview.AsNoTracking()
                .Include(r => r.Video)
                .AsSplitQuery()
                .FirstOrDefault(b => b.Id == Reviewid && b.TraineeId == userId);

            if (Data is null)
                return null;

            return new VideoReviewResponseFullData
            {
                Id = Data.Id,
                Review = Data.Review,
                Description = Data.Description,
                VideoId = Data.VideoId,
                VideoTitle = Data.Video.Tilte,
                TraineeId = Data.TraineeId,
                TraineeName = "You"
            };
        }
    }
}
