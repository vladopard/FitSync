using FitSync.DTOs;
using FluentValidation;

namespace FitSync.FluentValidation
{
    public abstract class PersonalRecordBaseValidator<T> : AbstractValidator<T>
        where T : PersonalRecordBaseDTO
    {
        protected PersonalRecordBaseValidator()
        {
            RuleFor(x => x.ExerciseId)
                .GreaterThan(0).WithMessage("ExerciseId must be greater than 0");

            RuleFor(x => x.MaxWeight)
                .GreaterThan(0).WithMessage("MaxWeight must be greater than 0");

            RuleFor(x => x.Reps)
                .GreaterThan(0).WithMessage("Reps must be greater than 0");

            RuleFor(x => x.AchievedAt)
                .NotEmpty().WithMessage("AchievedAt is required")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("AchievedAt cannot be in the future");
        }
    }

    public class PersonalRecordCreateValidator : PersonalRecordBaseValidator<PersonalRecordCreateDTO> { }

    public class PersonalRecordUpdateValidator : PersonalRecordBaseValidator<PersonalRecordUpdateDTO> { }

    
}

