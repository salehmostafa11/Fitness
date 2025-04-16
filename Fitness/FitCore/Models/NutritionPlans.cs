using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Models
{
    public class NutritionPlans
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public bool IsDeleted { get; set; } = false;

        //Trainee
        [ForeignKey("Trainee")]
        public string TraineeId { get; set; }
        public ApplicationUser Trainee { get; set; }

        //Nutritionist
        [ForeignKey("Nutritionist")]
        public string NutritionistId { get; set; }
        public ApplicationUser Nutritionist { get; set; }

        [ForeignKey("mealType")]
        public int? MealTypeId { get; set; }
        public MealType mealType { get; set; }

    }
}
