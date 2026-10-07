using EmployeeDirectoryApi.Dtos.Positions;

namespace EmployeeDirectoryApi.Services.Position.Interfaces;

public interface IPositionService
{
    Task<List<PositionDto>> GetAllPositions(CancellationToken ct = default);
    Task<PositionDto?> GetPosition(Guid uniqueId, CancellationToken ct = default);
    Task<PositionDto> CreatePosition(CreatePositionDto createPositionDto, CancellationToken ct = default);
}
