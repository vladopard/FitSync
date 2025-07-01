using FitSync.DTOs;

namespace FitSync.BusinessServices.Intefaces
{
    public interface IPersonalRecordService
    {
        Task<PersonalRecordDTO> CreateAsync(string userId, PersonalRecordCreateDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<PersonalRecordDTO>> GetAllByUserAsync(string userId);
        Task<PersonalRecordDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id, PersonalRecordUpdateDTO dto);
    }
}