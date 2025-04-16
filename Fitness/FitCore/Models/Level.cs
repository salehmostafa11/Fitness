using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Models
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ApplicationUser> usersTrainee { get; set; }
        public List<Video> videos { get; set; }
    }
}
