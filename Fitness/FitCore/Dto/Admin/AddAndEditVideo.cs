using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Dto.Admin
{
    public class AddAndEditVideo
    {
        public string VideoLink { get; set; }
        public string Tilte { get; set; }
        public int Level { get; set; }
        public int ExerciseTypeId { get; set; }


    }
}
