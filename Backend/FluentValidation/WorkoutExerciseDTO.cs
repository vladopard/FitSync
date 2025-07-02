using FitSync.DTOs;
using FluentValidation;

namespace FitSync.FluentValidation
{
    // 1) Заједнички базни валида­тор за Create и Update
    public abstract class WorkoutExerciseBaseValidator<T> : AbstractValidator<T>
        where T : WorkoutExerciseBaseDTO
    {
        protected WorkoutExerciseBaseValidator()
        {
           
            RuleFor(x => x.ExerciseId)
                .GreaterThan(0).WithMessage("ExerciseId must be greater than 0");

            RuleFor(x => x.Sets)
                .GreaterThan(0).WithMessage("Sets must be greater than 0");

            RuleFor(x => x.Reps)
                .GreaterThan(0).WithMessage("Reps must be greater than 0");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0");

            RuleFor(x => x.RestSeconds)
                .GreaterThanOrEqualTo(0).WithMessage("RestSeconds must be zero or positive");

            RuleFor(x => x.OrderInWorkout)
                .GreaterThan(0).WithMessage("OrderInWorkout must be greater than 0");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes must be at most 500 characters");
        }
    }

    public class WorkoutExerciseCreateValidator
        : WorkoutExerciseBaseValidator<WorkoutExerciseCreateDTO>
    {
        public WorkoutExerciseCreateValidator() : base() { }
    }
    public class WorkoutExerciseUpdateValidator
        : WorkoutExerciseBaseValidator<WorkoutExerciseUpdateDTO>
    {
        public WorkoutExerciseUpdateValidator() : base() { }
    }

}