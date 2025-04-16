using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Dto.Admin
{
    public class VideoView
    {
        public int Id { get; set; }
        public string VideoLink { get; set; }
        public string Tilte { get; set; }
        public int Level { get; set; }
        public string LevelText =>
            Level == 1 ? "Beginner" :
            Level == 2 ? "Intermediate" :
            Level == 3 ? "Advanced" :
            "Unknown";

        public int ExerciseTypeId { get; set; }
        public string ExerciseTypeText =>
            ExerciseTypeId == 1 ? "Shoulder" :
            ExerciseTypeId == 2 ? "Back" :
            ExerciseTypeId == 3 ? "Chest" :
            ExerciseTypeId == 4 ? "Legs" :
            ExerciseTypeId == 5 ? "Arms" :
            ExerciseTypeId == 6 ? "Core" :
            ExerciseTypeId == 7 ? "Cardio" :
            ExerciseTypeId == 8 ? "Abs" :
            ExerciseTypeId == 9 ? "Cardio" :
            "Unknown";

    }
}
