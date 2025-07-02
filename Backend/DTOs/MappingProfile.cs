using AutoMapper;
using FitSync.Entities;

namespace FitSync.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ======================
            // EXERCISE
            // ======================
            CreateMap<Exercise, ExerciseDTO>();

            CreateMap<ExerciseCreateDTO, Exercise>();
            CreateMap<ExerciseUpdateDTO, Exercise>();

            CreateMap<ExerciseDTO, ExercisePatchDTO>();

            CreateMap<ExercisePatchDTO, Exercise>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // ======================
            // EXERCISE PLAN
            // ======================
            CreateMap<ExercisePlan, ExercisePlanDTO>()
                .ForMember(dest => dest.Items,
                    opt => opt.MapFrom(src => src.Items));

            CreateMap<ExercisePlanCreateDTO, ExercisePlan>();

            CreateMap<ExercisePlanUpdateDTO, ExercisePlan>();

            CreateMap<ExercisePlanDTO, ExercisePlanPatchDTO>();

            CreateMap<ExercisePlanPatchDTO, ExercisePlan>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));

            // ======================
            // EXERCISE PLAN ITEM
            // ======================
            CreateMap<ExercisePlanItem, ExercisePlanItemDTO>()
                .ForMember(dest => dest.ExerciseName,
               opt => opt.MapFrom(src => src.Exercise.Name))
                .ForMember(dest => dest.PlanName,
               opt => opt.MapFrom(src => src.Plan.Name));


            CreateMap<ExercisePlanItemCreateDTO, ExercisePlanItem>();
            CreateMap<ExercisePlanItemUpdateDTO, ExercisePlanItem>();

            CreateMap<ExercisePlanItemDTO, ExercisePlanItemPatchDTO>();

            CreateMap<ExercisePlanItemPatchDTO, ExercisePlanItem>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));
            // ======================
            // PERSONAL RECORD
            // ======================
            CreateMap<PersonalRecord, PersonalRecordDTO>()
            .ForMember(dest => dest.ExerciseName, opt => opt.MapFrom(src => src.Exercise.Name));

            CreateMap<PersonalRecordCreateDTO, PersonalRecord>();
            CreateMap<PersonalRecordUpdateDTO, PersonalRecord>();

            // ======================
            // WORKOUT
            // ======================
            CreateMap<Workout, WorkoutDTO>()
                .ForMember(dest => dest.PlanName,
                    opt => opt.MapFrom(src => src.ExercisePlan != null ? src.ExercisePlan.Name : null))
                .ForMember(dest => dest.Exercises,
                    opt => opt.MapFrom(src => src.Exercises));

            // ─── DTO → Entity (Create / Update) ────────────────────────────────────

            CreateMap<WorkoutCreateDTO, Workout>()
                // these fields are set in the service, not from client
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                // we don’t touch the navigation collections here
                .ForMember(dest => dest.Exercises, opt => opt.Ignore())
                .ForMember(dest => dest.ExercisePlan, opt => opt.Ignore());

            CreateMap<WorkoutUpdateDTO, Workout>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Exercises, opt => opt.Ignore())
                .ForMember(dest => dest.ExercisePlan, opt => opt.Ignore());

            // ─── DTO → Entity (Patch) NOT IMPLEMENTED ──────────────────────────────────────────────

            // ======================
            // WORKOUT EXERCISE
            // ======================
            CreateMap<WorkoutExercise, WorkoutExerciseDTO>()
               .ForMember(dest => dest.ExerciseName,
                          opt => opt.MapFrom(src => src.Exercise.Name));

            // ─── DTO → Entity (Create / Update) ────────────────────────────────────

            CreateMap<WorkoutExerciseCreateDTO, WorkoutExercise>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Workout, opt => opt.Ignore())
                .ForMember(dest => dest.Exercise, opt => opt.Ignore());

            CreateMap<WorkoutExerciseUpdateDTO, WorkoutExercise>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Workout, opt => opt.Ignore())
                .ForMember(dest => dest.Exercise, opt => opt.Ignore());

        }
    }
}
