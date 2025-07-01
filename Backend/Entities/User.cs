using Microsoft.AspNetCore.Identity;

namespace FitSync.Entities
{
    public class User : IdentityUser
    {
        public ICollection<PersonalRecord> PersonalRecords { get; set; } = new List<PersonalRecord>();

        // Веза ка плановима вежби
        public ICollection<ExercisePlan> ExercisePlans { get; set; } = new List<ExercisePlan>();

        // Веза ка одрађеним тренинзима
        public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    }
}
