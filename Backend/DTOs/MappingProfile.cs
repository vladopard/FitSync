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
        }
    }
}
