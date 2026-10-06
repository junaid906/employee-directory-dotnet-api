namespace EmployeeDirectoryApi.Entities;

public class EmployeePositionEntity : BaseEntity
{
    public string JobTitle { get; private set; }
    public long DepartmentId { get; private set; }
    public long? SubDepartmentId { get; private set; }
    public long? ReportToPositionId { get; private set; }
    public int SeatingPosition { get; private set; }
    
    public EmployeePositionEntity( ){}
    
    public EmployeePositionEntity(
        string jobTitle,
        long department,
        long? subDepartment,
        long? reportToPosition,
        int seatingPosition
    )
    {
        JobTitle = jobTitle;
        DepartmentId = department;
        SubDepartmentId = subDepartment;
        ReportToPositionId = reportToPosition;
        SeatingPosition = seatingPosition;
    }

    public void UpdateJobTitle(string jobTitle)
    {
        JobTitle = jobTitle;
    }
    
    public void UpdateDepartmentId(long departmentId)
    {
        DepartmentId = departmentId;
    }
    
    public void UpdateSubDepartmentId(long subDepartmentId)
    {
        SubDepartmentId = subDepartmentId;
    }

    public void UpdateReportToPositionId(long reportToPositionId)
    {
        ReportToPositionId = reportToPositionId;
    }

    public void UpdateSeatingPosition(int seatingPosition)
    {
        SeatingPosition = seatingPosition;
    }
}