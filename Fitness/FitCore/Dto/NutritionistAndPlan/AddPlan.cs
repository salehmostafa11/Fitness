using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Dto.NutritionistAndPlan
{
    public class AddPlan
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public int? MealTypeId { get; set; }
        public string TraineeId { get; set; }

    }
}
