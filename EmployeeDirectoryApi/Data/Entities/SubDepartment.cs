namespace EmployeeDirectoryApi.Entities;

public class SubDepartment : BaseEntity
{
    public string SubDepartmentName { get; private set; }
    public long DepartmentId { get; private set; }
    public Department? Department { get; private set; }

    public SubDepartment( ){}
    
    public SubDepartment(
        string subDepartmentName,
        long departmentId
    )
    {
        SubDepartmentName = subDepartmentName;
        DepartmentId = departmentId;
    }
}



