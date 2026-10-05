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

        dbContext.Employees.AddRange(EmployeeSeed.Employees());
        await dbContext.SaveChangesAsync(ct);

        await dbContext.Database.ExecuteSqlRawAsync(
            """
            SELECT setval(
                pg_get_serial_sequence('"Employees"', 'Id'),
                (SELECT COALESCE(MAX("Id"), 1) FROM "Employees")
            );
            """,
            ct);
    }
}
