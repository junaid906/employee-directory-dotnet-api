using EmployeeDirectoryApi.Entities;

namespace EmployeeDirectoryApi.Data;

public static class EmployeeSeed
{
    public static List<EmployeeEntity> Employees()
    {
        var ada = new EmployeeEntity { FirstName = "Ada", LastName = "Lovelace", Email = "ada@example.com" };
        ada.SetId(1);
        var alan = new EmployeeEntity { FirstName = "Alan", LastName = "Turing", Email = "alan@example.com" };
        alan.SetId(2);
        return [ada, alan];
    }
}