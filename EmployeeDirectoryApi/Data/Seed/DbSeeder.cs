using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryApi.Data.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(EmployeeDirectoryDbContext dbContext, CancellationToken ct = default)
    {
        if (await dbContext.Employees.AnyAsync(ct))
        {
            return;
        }

        var seed = EmployeeSeed.Build();

        dbContext.Departments.AddRange(seed.Departments);
        await dbContext.SaveChangesAsync(ct);

        dbContext.SubDepartments.AddRange(seed.SubDepartments);
        await dbContext.SaveChangesAsync(ct);

        // Insert positions first with no parent, then wire the tree, so the self-referencing
        // foreign key never points at a row that has not been inserted yet.
        dbContext.EmployeePositions.AddRange(seed.Positions);
        await dbContext.SaveChangesAsync(ct);

        var positionsById = seed.Positions.ToDictionary(position => position.Id);
        foreach (var (positionId, parentPositionId) in seed.PositionParentByPositionId)
        {
            positionsById[positionId].UpdateReportToPositionId(parentPositionId);
        }

        await dbContext.SaveChangesAsync(ct);

        dbContext.Employees.AddRange(seed.Employees);
        await dbContext.SaveChangesAsync(ct);

        await ResetIdentitySequences(dbContext, ct);
    }

    private static async Task ResetIdentitySequences(EmployeeDirectoryDbContext dbContext, CancellationToken ct)
    {
        await dbContext.Database.ExecuteSqlRawAsync(
            """SELECT setval(pg_get_serial_sequence('"Departments"', 'Id'), (SELECT COALESCE(MAX("Id"), 1) FROM "Departments"));""",
            ct);
        await dbContext.Database.ExecuteSqlRawAsync(
            """SELECT setval(pg_get_serial_sequence('"SubDepartments"', 'Id'), (SELECT COALESCE(MAX("Id"), 1) FROM "SubDepartments"));""",
            ct);
        await dbContext.Database.ExecuteSqlRawAsync(
            """SELECT setval(pg_get_serial_sequence('"EmployeePositions"', 'Id'), (SELECT COALESCE(MAX("Id"), 1) FROM "EmployeePositions"));""",
            ct);
        await dbContext.Database.ExecuteSqlRawAsync(
            """SELECT setval(pg_get_serial_sequence('"Employees"', 'Id'), (SELECT COALESCE(MAX("Id"), 1) FROM "Employees"));""",
            ct);
    }
}
