using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Dto.NutritionistAndPlan
{
    public class ViewPlansById
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int? MealTypeId { get; set; }
        public string MealTypeText =>
            MealTypeId == 1 ? "Breakfast" :
            MealTypeId == 2 ? "Dinner" :
            MealTypeId == 3 ? "Lunch" :
            MealTypeId == 4 ? "Snack" :
            "Unknown";
        public string TraineeId { get; set; }
        public string? TraineeName { get; set; }
        public string NutritionistId { get; set; }
        public string? NutritionistName { get; set; }

    }
}
