namespace EmployeeDirectoryApi.Entities;

public class DepartmentEntity : BaseEntity
{
    public string DepartmentName { get; private set; }
    
    public DepartmentEntity( ){}
    
    public DepartmentEntity(
        string departmentName
    )
    {
        DepartmentName = departmentName;
    }
}