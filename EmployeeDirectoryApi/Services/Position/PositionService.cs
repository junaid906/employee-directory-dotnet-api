using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Dtos.Positions;
using EmployeeDirectoryApi.Entities;
using EmployeeDirectoryApi.Services.Position.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Services.Position;

public class PositionService : IPositionService
{
    private readonly EmployeeDirectoryDbContext _dbContext;

    public PositionService(EmployeeDirectoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Get All Positions

    public async Task<List<PositionDto>> GetAllPositions(CancellationToken ct = default)
    {
        var positions = await _dbContext.EmployeePositions
            .AsNoTracking()
            .Where(p => p.Active)
            .ToListAsync(ct);

        var uniqueIdByDepartmentId = await _dbContext.Departments
            .AsNoTracking()
            .ToDictionaryAsync(d => d.Id, d => d.UniqueId, ct);

        var uniqueIdBySubDepartmentId = await _dbContext.SubDepartments
            .AsNoTracking()
            .ToDictionaryAsync(s => s.Id, s => s.UniqueId, ct);

        var uniqueIdByPositionId = positions.ToDictionary(p => p.Id, p => p.UniqueId);

        return positions
            .Select(p => Map(
                p,
                uniqueIdByDepartmentId[p.DepartmentId],
                p.SubDepartmentId is { } subDepartmentId ? uniqueIdBySubDepartmentId[subDepartmentId] : null,
                p.ReportToPositionId is { } parentId && uniqueIdByPositionId.TryGetValue(parentId, out var parentUniqueId) ? parentUniqueId : null))
            .ToList();
    }

    #endregion

    #region Get Position

    public async Task<PositionDto?> GetPosition(Guid uniqueId, CancellationToken ct = default)
    {
        var position = await _dbContext.EmployeePositions
            .AsNoTracking()
            .Where(p => p.Active)
            .FirstOrDefaultAsync(p => p.UniqueId == uniqueId, ct);

        if (position is null)
        {
            return null;
        }

        var departmentUniqueId = await _dbContext.Departments
            .Where(d => d.Id == position.DepartmentId)
            .Select(d => d.UniqueId)
            .FirstOrDefaultAsync(ct);

        Guid? subDepartmentUniqueId = null;
        if (position.SubDepartmentId is { } subDepartmentId)
        {
            subDepartmentUniqueId = await _dbContext.SubDepartments
                .Where(s => s.Id == subDepartmentId)
                .Select(s => (Guid?)s.UniqueId)
                .FirstOrDefaultAsync(ct);
        }

        Guid? reportToPositionUniqueId = null;
        if (position.ReportToPositionId is { } parentId)
        {
            reportToPositionUniqueId = await _dbContext.EmployeePositions
                .Where(p => p.Id == parentId)
                .Select(p => (Guid?)p.UniqueId)
                .FirstOrDefaultAsync(ct);
        }

        return Map(position, departmentUniqueId, subDepartmentUniqueId, reportToPositionUniqueId);
    }

    #endregion

    #region Create Position

    public async Task<PositionDto> CreatePosition(CreatePositionDto createPositionDto, CancellationToken ct = default)
    {
        var departmentId = await _dbContext.Departments
            .Where(d => d.UniqueId == createPositionDto.DepartmentUniqueId)
            .Select(d => (long?)d.Id)
            .FirstOrDefaultAsync(ct);

        if (departmentId is null)
        {
            throw new ArgumentException($"No department found for {createPositionDto.DepartmentUniqueId}");
        }

        long? subDepartmentId = null;
        if (createPositionDto.SubDepartmentUniqueId is { } subDepartmentUniqueId)
        {
            subDepartmentId = await _dbContext.SubDepartments
                .Where(s => s.UniqueId == subDepartmentUniqueId)
                .Select(s => (long?)s.Id)
                .FirstOrDefaultAsync(ct);

            if (subDepartmentId is null)
            {
                throw new ArgumentException($"No subdepartment found for {subDepartmentUniqueId}");
            }
        }

        long? reportToPositionId = null;
        if (createPositionDto.ReportToPositionUniqueId is { } reportToPositionUniqueId)
        {
            reportToPositionId = await _dbContext.EmployeePositions
                .Where(p => p.UniqueId == reportToPositionUniqueId)
                .Select(p => (long?)p.Id)
                .FirstOrDefaultAsync(ct);

            if (reportToPositionId is null)
            {
                throw new ArgumentException($"No position found for {reportToPositionUniqueId}");
            }
        }

        var position = new EmployeePositionEntity(
            createPositionDto.JobTitle,
            departmentId.Value,
            subDepartmentId,
            reportToPositionId,
            createPositionDto.SeatingPosition);

        _dbContext.EmployeePositions.Add(position);
        await _dbContext.SaveChangesAsync(ct);

        return Map(
            position,
            createPositionDto.DepartmentUniqueId,
            createPositionDto.SubDepartmentUniqueId,
            createPositionDto.ReportToPositionUniqueId);
    }

    #endregion

    private static PositionDto Map(
        EmployeePositionEntity position,
        Guid departmentUniqueId,
        Guid? subDepartmentUniqueId,
        Guid? reportToPositionUniqueId) => new()
    {
        UniqueId = position.UniqueId,
        JobTitle = position.JobTitle,
        DepartmentUniqueId = departmentUniqueId,
        SubDepartmentUniqueId = subDepartmentUniqueId,
        ReportToPositionUniqueId = reportToPositionUniqueId,
        SeatingPosition = position.SeatingPosition
    };
}
