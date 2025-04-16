using FitCore.IRepositories;
using FitData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitData.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAuthServices AuthServices { get; private set; }
        public ISearch Search { get; set; }
        public INutritionistServices NutritionistServices { get; set; }
        public IVideoService VideoService { get; set; }
        public IVideoReviewServices VideoReviewServices { get; set; }



        public UnitOfWork(ApplicationDbContext context,
            IAuthServices authServices , ISearch search,
            INutritionistServices nutritionistServices, 
            IVideoService videoService
            , IVideoReviewServices videoReviewServices)
        {
            _context = context;
            AuthServices = authServices;
            Search = search;
            NutritionistServices = nutritionistServices;
            VideoService = videoService;
            VideoReviewServices = videoReviewServices;  
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
