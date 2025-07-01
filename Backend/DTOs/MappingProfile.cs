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

            // allow mapping back from DTO to patch DTO if needed
            CreateMap<ExercisePlanItemDTO, ExercisePlanItemPatchDTO>();

            CreateMap<ExercisePlanItemPatchDTO, ExercisePlanItem>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));
            // ======================
            // PERSONAL RECORD
            // ======================
            CreateMap<PersonalRecord, PersonalRecordDTO>()
            .ForMember(dest => dest.ExerciseName, opt => opt.MapFrom(src => src.Exercise.Name));

            // Mapiranja за Create и Update DTO → PersonalRecord
            CreateMap<PersonalRecordCreateDTO, PersonalRecord>();
            CreateMap<PersonalRecordUpdateDTO, PersonalRecord>();

        }
    }
}
