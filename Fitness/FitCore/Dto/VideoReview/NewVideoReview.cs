using FitCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Dto.VideoReview
{
    public class NewVideoReview
    {
        public int Review { get; set; }
        public string? Description { get; set; }
        public int VideoId { get; set; }
    }
}
