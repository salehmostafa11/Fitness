using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Dto.VideoReview
{
    public class VideoReviewResponseFullData
    {
        public int Id { get; set; }
        public int Review { get; set; }
        public string Description { get; set; }
        public int VideoId { get; set; }
        public string VideoTitle { get; set; }
        public string TraineeId { get; set; }
        public string TraineeName { get; set; }
    }
}
