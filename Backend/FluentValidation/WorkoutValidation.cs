using FitSync.DTOs;
using FluentValidation;

namespace FitSync.FluentValidation
{
    // 1) Заједнички базни валида­тор за Create и Update
    public abstract class WorkoutBaseValidator<T> : AbstractValidator<T>
        where T : WorkoutBaseDTO
    {
        protected WorkoutBaseValidator()
        {

            RuleFor(x => x.ExercisePlanId)
                .GreaterThan(0)
                .When(x => x.ExercisePlanId.HasValue)
                .WithMessage("ExercisePlanId must be greater than 0");
        }
    }

    // 2) Validator за креирање (POST)
    public class WorkoutCreateValidator
        : WorkoutBaseValidator<WorkoutCreateDTO>
    {
        public WorkoutCreateValidator() : base() { }
    }

    // 3) Validator за потпуно ажурирање (PUT)
    public class WorkoutUpdateValidator
        : WorkoutBaseValidator<WorkoutUpdateDTO>
    {
        public WorkoutUpdateValidator() : base() { }
    }

    // 4) Validator за делимично ажурирање (PATCH)
    public class WorkoutPatchValidator
        : AbstractValidator<WorkoutPatchDTO>
    {
        public WorkoutPatchValidator()
        {
            RuleFor(x => x.ExercisePlanId)
                .GreaterThan(0)
                .When(x => x.ExercisePlanId.HasValue)
                .WithMessage("ExercisePlanId must be greater than 0");
        }
    }
}
