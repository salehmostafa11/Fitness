using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Dto.NutritionistAndPlan
{
    public class ViewPlans
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int? MealType { get; set; }
        public string MealTypeText =>
            MealType == 1 ? "Breakfast" :
            MealType == 2 ? "Dinner" :
            MealType == 3 ? "Lunch" :
            MealType == 4 ? "Snack" :
            "Unknown";
        public string TraineeId { get; set; }
        public string? TraineeName { get; set; }
        public string NutritionistId { get; set; }
        public string NutritionistName { get; set; }
    }
}
