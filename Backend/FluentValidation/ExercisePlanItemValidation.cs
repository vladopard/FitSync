using FitSync.DTOs;
using FluentValidation;

namespace FitSync.FluentValidation
{
    public abstract class ExercisePlanItemBaseValidator<T> : AbstractValidator<T>
        where T : ExercisePlanItemBaseDTO
    {
        protected ExercisePlanItemBaseValidator()
        {
            RuleFor(x => x.ExercisePlanId)
                .GreaterThan(0).WithMessage("ExercisePlanId must be greater than 0");

            RuleFor(x => x.ExerciseId)
                .GreaterThan(0).WithMessage("ExerciseId must be greater than 0");

            RuleFor(x => x.Order)
                .GreaterThan(0).WithMessage("Order must be greater than 0");

            RuleFor(x => x.Sets)
                .GreaterThan(0).WithMessage("Sets must be greater than 0");

            RuleFor(x => x.Reps)
                .GreaterThan(0).WithMessage("Reps must be greater than 0");

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Note must be at most 500 characters");
        }
    }
    public class ExercisePlanItemCreateValidator
        : ExercisePlanItemBaseValidator<ExercisePlanItemCreateDTO>
    {
        public ExercisePlanItemCreateValidator() : base() { }
    }
    public class ExercisePlanItemUpdateValidator
        : ExercisePlanItemBaseValidator<ExercisePlanItemUpdateDTO>
    {
        public ExercisePlanItemUpdateValidator() : base() { }
    }
}