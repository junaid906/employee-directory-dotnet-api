using EmployeeDirectoryApi.Data;
using EmployeeDirectoryApi.Dtos.Positions;
using EmployeeDirectoryApi.Entities;
using EmployeeDirectoryApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Services.Position;

public class PositionService
{
    private readonly EmployeeDirectoryDbContext _dbContext;

    public PositionService(EmployeeDirectoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PositionDto>> GetAllPositions(CancellationToken ct = default)
    {
        return await _dbContext.EmployeePositions
            .AsNoTracking()
            .Where(p => p.Active)
            .Select(PositionDto.QueryProjection)
            .ToListAsync(ct);
    }

    public async Task<PositionDto?> GetPosition(Guid uniqueId, CancellationToken ct = default)
    {
        return await _dbContext.EmployeePositions
            .AsNoTracking()
            .Where(p => p.Active && p.UniqueId == uniqueId)
            .Select(PositionDto.QueryProjection)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PositionDto> CreatePosition(CreatePositionDto createPositionDto, CancellationToken ct = default)
    {
        var departmentId = await ResolveDepartmentId(createPositionDto.DepartmentUniqueId, ct);
        var subDepartmentId = await ResolveSubDepartmentId(createPositionDto.SubDepartmentUniqueId, ct);
        var reportToPositionId = await ResolvePositionId(createPositionDto.ReportToPositionUniqueId, ct);

        var position = new EmployeePositionEntity(
            createPositionDto.JobTitle,
            departmentId,
            subDepartmentId,
            reportToPositionId,
            createPositionDto.SeatingPosition);

        _dbContext.EmployeePositions.Add(position);
        await _dbContext.SaveChangesAsync(ct);

        return PositionDto.FromEntity(
            position,
            createPositionDto.DepartmentUniqueId,
            createPositionDto.SubDepartmentUniqueId,
            createPositionDto.ReportToPositionUniqueId);
    }

    public async Task<PositionDto> InsertPosition(InsertPositionDto insertPositionDto, CancellationToken ct = default)
    {
        var departmentId = await ResolveDepartmentId(insertPositionDto.DepartmentUniqueId, ct);
        var subDepartmentId = await ResolveSubDepartmentId(insertPositionDto.SubDepartmentUniqueId, ct);
        var reportToPositionId = await ResolvePositionId(insertPositionDto.ReportToPositionUniqueId, ct);

        var children = await LoadChildrenToReassign(
            insertPositionDto.ChildPositionUniqueIds,
            reportToPositionId,
            ct);

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

        var position = new EmployeePositionEntity(
            insertPositionDto.JobTitle,
            departmentId,
            subDepartmentId,
            reportToPositionId,
            insertPositionDto.SeatingPosition);

        _dbContext.EmployeePositions.Add(position);
        await _dbContext.SaveChangesAsync(ct);

        foreach (var child in children)
        {
            child.UpdateReportToPositionId(position.Id);
            child.IsUpdated();
        }

        if (children.Count > 0)
        {
            await _dbContext.SaveChangesAsync(ct);
        }

        await transaction.CommitAsync(ct);

        return PositionDto.FromEntity(
            position,
            insertPositionDto.DepartmentUniqueId,
            insertPositionDto.SubDepartmentUniqueId,
            insertPositionDto.ReportToPositionUniqueId);
    }

    public async Task<PositionDto?> UpdatePosition(Guid uniqueId, UpdatePositionDto updatePositionDto, CancellationToken ct = default)
    {
        var position = await _dbContext.EmployeePositions
            .FirstOrDefaultAsync(p => p.UniqueId == uniqueId && p.Active, ct);

        if (position is null)
        {
            return null;
        }

        var departmentId = await ResolveDepartmentId(updatePositionDto.DepartmentUniqueId, ct);
        var subDepartmentId = await ResolveSubDepartmentId(updatePositionDto.SubDepartmentUniqueId, ct);
        var reportToPositionId = await ResolvePositionId(updatePositionDto.ReportToPositionUniqueId, ct);

        if (reportToPositionId != position.ReportToPositionId)
        {
            await EnsureNoCycle(position.Id, reportToPositionId, ct);
        }

        position.UpdateJobTitle(updatePositionDto.JobTitle);
        position.UpdateDepartmentId(departmentId);
        position.UpdateSubDepartmentId(subDepartmentId);
        position.UpdateReportToPositionId(reportToPositionId);
        position.UpdateSeatingPosition(updatePositionDto.SeatingPosition);
        position.IsUpdated();

        await _dbContext.SaveChangesAsync(ct);

        return PositionDto.FromEntity(
            position,
            updatePositionDto.DepartmentUniqueId,
            updatePositionDto.SubDepartmentUniqueId,
            updatePositionDto.ReportToPositionUniqueId);
    }

    private async Task<List<EmployeePositionEntity>> LoadChildrenToReassign(
        List<Guid> childPositionUniqueIds,
        long? reportToPositionId,
        CancellationToken ct)
    {
        if (childPositionUniqueIds.Count == 0)
        {
            return [];
        }

        if (reportToPositionId is null)
        {
            throw new ArgumentException("Children cannot be reassigned when the new position has no parent.");
        }

        var distinctIds = childPositionUniqueIds.Distinct().ToList();

        var children = await _dbContext.EmployeePositions
            .Where(p => p.Active && distinctIds.Contains(p.UniqueId))
            .ToListAsync(ct);

        if (children.Count != distinctIds.Count)
        {
            var found = children.Select(child => child.UniqueId).ToHashSet();
            var missing = distinctIds.Where(id => !found.Contains(id));
            throw new ArgumentException($"Position(s) not found: {string.Join(", ", missing)}");
        }

        var notReportingToParent = children
            .Where(child => child.ReportToPositionId != reportToPositionId)
            .Select(child => child.UniqueId)
            .ToList();

        if (notReportingToParent.Count > 0)
        {
            throw new ArgumentException(
                $"Position(s) do not report to the chosen parent: {string.Join(", ", notReportingToParent)}");
        }

        return children;
    }

    private async Task EnsureNoCycle(long positionId, long? newParentId, CancellationToken ct)
    {
        if (newParentId is null)
        {
            return;
        }

        if (newParentId == positionId)
        {
            throw new PositionCycleException("A position cannot report to itself.");
        }

        var parentByPositionId = await _dbContext.EmployeePositions
            .AsNoTracking()
            .Select(p => new { p.Id, p.ReportToPositionId })
            .ToDictionaryAsync(p => p.Id, p => p.ReportToPositionId, ct);

        var visited = new HashSet<long>();
        var currentId = newParentId.Value;

        while (true)
        {
            if (currentId == positionId)
            {
                throw new PositionCycleException("This change would create a cycle in the reporting structure.");
            }

            if (!visited.Add(currentId))
            {
                return;
            }

            if (!parentByPositionId.TryGetValue(currentId, out var parentId) || parentId is null)
            {
                return;
            }

            currentId = parentId.Value;
        }
    }

    private async Task<long> ResolveDepartmentId(Guid uniqueId, CancellationToken ct)
    {
        var departmentId = await _dbContext.Departments
            .Where(d => d.UniqueId == uniqueId)
            .Select(d => (long?)d.Id)
            .FirstOrDefaultAsync(ct);

        return departmentId ?? throw new ArgumentException($"No department found for {uniqueId}");
    }

    private async Task<long?> ResolveSubDepartmentId(Guid? uniqueId, CancellationToken ct)
    {
        if (uniqueId is null)
        {
            return null;
        }

        var subDepartmentId = await _dbContext.SubDepartments
            .Where(s => s.UniqueId == uniqueId)
            .Select(s => (long?)s.Id)
            .FirstOrDefaultAsync(ct);

        return subDepartmentId ?? throw new ArgumentException($"No subdepartment found for {uniqueId}");
    }

    private async Task<long?> ResolvePositionId(Guid? uniqueId, CancellationToken ct)
    {
        if (uniqueId is null)
        {
            return null;
        }

        var positionId = await _dbContext.EmployeePositions
            .Where(p => p.UniqueId == uniqueId)
            .Select(p => (long?)p.Id)
            .FirstOrDefaultAsync(ct);

        return positionId ?? throw new ArgumentException($"No position found for {uniqueId}");
    }
}
