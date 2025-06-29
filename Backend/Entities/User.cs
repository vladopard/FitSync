using Microsoft.AspNetCore.Identity;

namespace FitSync.Entities
{
    public class User : IdentityUser
    {
        public ICollection<PersonalRecord> PersonalRecords { get; set; } = new();

        // Веза ка плановима вежби
        public ICollection<ExercisePlan> ExercisePlans { get; set; } = new();

        // Веза ка одрађеним тренинзима
        public ICollection<Workout> Workouts { get; set; } = new();
    }
}
