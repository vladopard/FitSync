using FitSync.DTOs;
using FluentValidation;

namespace FitSync.FluentValidation
{
    public abstract class ExercisePlanBaseValidator<T> : AbstractValidator<T>
        where T : ExercisePlanBaseDTO
    {
        protected ExercisePlanBaseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must be at most 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must be at most 500 characters");
        }
    }

    public class ExercisePlanCreateValidator : ExercisePlanBaseValidator<ExercisePlanCreateDTO> 
    {
        public ExercisePlanCreateValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage("UserId is required");
        }

    }

    public class ExercisePlanUpdateValidator : ExercisePlanBaseValidator<ExercisePlanUpdateDTO> { }
}
