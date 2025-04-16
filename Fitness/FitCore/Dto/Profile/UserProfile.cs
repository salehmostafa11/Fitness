using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Dto.Profile
{
    public class UserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public int? Gender { get; set; }
        public string GenderText => Gender == 0 ? "Male" : "Female";
        public int? Level { get; set; }
        public string LevelText =>
            Level == 1 ? "Beginner" :
            Level == 2 ? "Intermediate" :
            Level == 3 ? "Advanced" :
            "Unknown";
    }
}
