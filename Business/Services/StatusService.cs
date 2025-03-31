using Business.Factories;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<IEnumerable<StatusEntity>> GetAllAsync()
    {
        var entities = await _statusRepository.GetAllAsync();

        return entities;
    }

}
