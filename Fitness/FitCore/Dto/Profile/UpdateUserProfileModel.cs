using FitCore.Dto.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Dto.Profile
{
    public class UpdateUserProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Height { get; set; } 
        public decimal Weight { get; set; }
        public Gender Gender { get; set; }
    }
}
