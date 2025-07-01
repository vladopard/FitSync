using AutoMapper;
using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using FitSync.Entities;

namespace FitSync.BusinessServices;

public class PersonalRecordService : IPersonalRecordService
{
    private readonly IFitSyncRepository _repo;
    private readonly IMapper _mapper;

    public PersonalRecordService(IFitSyncRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PersonalRecordDTO>> GetAllByUserAsync(string userId)
    {
        var records = await _repo.GetAllPersonalRecordsByUserAsync(userId);
        return _mapper.Map<IEnumerable<PersonalRecordDTO>>(records);
    }

    public async Task<PersonalRecordDTO> GetByIdAsync(int id)
    {
        var record = await GetRecordOrThrowAsync(id);
        return _mapper.Map<PersonalRecordDTO>(record);
    }

    public async Task<PersonalRecordDTO> CreateAsync(string userId, PersonalRecordCreateDTO dto)
    {
        var record = _mapper.Map<PersonalRecord>(dto);
        record.UserId = userId;

        await _repo.AddPersonalRecordAsync(record);
        await _repo.SaveChangesAsync();

        return _mapper.Map<PersonalRecordDTO>(record);
    }

    public async Task UpdateAsync(int id, PersonalRecordUpdateDTO dto)
    {
        var record = await GetRecordOrThrowAsync(id);
        _mapper.Map(dto, record);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var record = await GetRecordOrThrowAsync(id);
        _repo.DeletePersonalRecord(record);
        await _repo.SaveChangesAsync();
    }

    private async Task<PersonalRecord> GetRecordOrThrowAsync(int id)
        => await _repo.GetPersonalRecordByIdAsync(id)
           ?? throw new KeyNotFoundException($"Record with id {id} not found.");
}
