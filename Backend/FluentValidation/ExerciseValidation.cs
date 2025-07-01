using FluentValidation;
using FitSync.DTOs;

namespace FitSync.Validation.ExerciseValidation
{
    // Base validator
    public abstract class ExerciseBaseValidator<T> : AbstractValidator<T>
        where T : ExerciseBaseDTO
    {
        protected ExerciseBaseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must be at most 100 characters");

            RuleFor(x => x.MuscleGroup)
                .NotEmpty().WithMessage("Muscle group is required")
                .MaximumLength(100).WithMessage("Muscle group must be at most 100 characters");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required")
                .MaximumLength(100).WithMessage("Type must be at most 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Description must be at most 1000 characters");
        }
    }

    // Create validator
    public class ExerciseCreateValidator : ExerciseBaseValidator<ExerciseCreateDTO>
    {
        public ExerciseCreateValidator() : base() { }
    }

    // Update validator
    public class ExerciseUpdateValidator : ExerciseBaseValidator<ExerciseUpdateDTO>
    {
        public ExerciseUpdateValidator() : base() { }
    }

    // Patch validator (optional fields)
    public class ExercisePatchValidator : AbstractValidator<ExercisePatchDTO>
    {
        public ExercisePatchValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name must be at most 100 characters");

            RuleFor(x => x.MuscleGroup)
                .MaximumLength(100).WithMessage("Muscle group must be at most 100 characters");

            RuleFor(x => x.Type)
                .MaximumLength(100).WithMessage("Type must be at most 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must be at most 1000 characters");
        }
    }
}
