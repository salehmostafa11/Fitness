using FitCore.Dto.VideoReview;
using FitCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.IRepositories
{
    public interface IVideoReviewServices
    {
        Task<VideoReviewResponse> AddVideoReview(NewVideoReview model, string userId);
        Task<VideoReviewResponse> EditVideoReview(EditVideoReview model, string userId, int videoId);
        Task<VideoReviewResponse> DeleteVideoReview(string userId, int videoId);
        Task<VideoReviewResponseFullData> GetVideoReviewById(int Reviewid);
        Task<VideoReviewResponseFullData> GetVideoReviewById(int Reviewid,string userId);
        Task<List<VideoReviewResponseFullData>> GetAllMyVideoReview(string userId);
    }
}
