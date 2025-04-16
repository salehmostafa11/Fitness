using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Models
{
    public class TraineerReview
    {
        public int Id { get; set; }
        public int Review { get; set; }
        public string Description { get; set; }


        [ForeignKey("Trainer")]
        public string TrainerId { get; set; }
        public ApplicationUser Trainer { get; set; }

        [ForeignKey("Trainee")]
        public string TraineeId { get; set; }
        public ApplicationUser Trainee { get; set; }
    }
}
