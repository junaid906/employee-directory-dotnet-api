namespace EmployeeDirectoryApi.Entities;

public class Department : BaseEntity
{
    public string DepartmentName { get; private set; }
    
    public Department( ){}
    
    public Department(
        string departmentName
    )
    {
        DepartmentName = departmentName;
    }
}