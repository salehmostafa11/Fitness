using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Dto.Authentication
{
    public class RegisterTrainerAndNutritionistModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int ExperienceYears { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
