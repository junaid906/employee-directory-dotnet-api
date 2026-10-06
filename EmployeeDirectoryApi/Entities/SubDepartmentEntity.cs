namespace EmployeeDirectoryApi.Entities;

public class SubDepartmentEntity : BaseEntity
{
    public string SubDepartmentName { get; private set; }
    public long DepartmentId { get; private set; }

    public SubDepartmentEntity( ){}
    
    public SubDepartmentEntity(
        string subDepartmentName,
        long departmentId
    )
    {
        SubDepartmentName = subDepartmentName;
        DepartmentId = departmentId;
    }
}



