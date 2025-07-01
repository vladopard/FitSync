namespace FitSync.DTOs
{
    public abstract class PersonalRecordBaseDTO
    {
        public required int ExerciseId { get; set; }
        public required double MaxWeight { get; set; }
        public required int Reps { get; set; }
        public required DateTime AchievedAt { get; set; }
    }

    public class PersonalRecordCreateDTO : PersonalRecordBaseDTO { }

    public class PersonalRecordUpdateDTO : PersonalRecordBaseDTO { }

    public class PersonalRecordDTO : PersonalRecordBaseDTO
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; } = null!;
    }
}
