using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Models
{
    public class MealType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<NutritionPlans> nutritionPlans { get; set; }

    }
}
