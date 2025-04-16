using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string VideoLink { get; set; }
        public string  Tilte { get; set; }

        // public bool IsDelete { get; set; }
        public int LevelId { get; set; }
        public Level level { get; set; }

        [ForeignKey("ExerciseType")]
        public int ExerciseTypeId { get; set; }
        public ExerciseType ExerciseType { get; set; }

        [ForeignKey("Trainner")]
        public string TrainnerId { get; set; }
        public ApplicationUser Trainner { get; set; }

        // M : M Video and VideoReview
        public ICollection<VideoReview> VideoReview { get; set; }


    }
}
