namespace EmployeeDirectoryApi.Entities;

public class EmployeePosition : BaseEntity
{
    public string JobTitle { get; private set; }
    public long DepartmentId { get; private set; }
    public Department? Department { get; private set; }
    public long? SubDepartmentId { get; private set; }
    public SubDepartment? SubDepartment { get; private set; }
    public long? ReportToPositionId { get; private set; }
    public EmployeePosition? ReportToPosition { get; private set; }

    public ICollection<EmployeePosition> ChildPositions { get; } = new List<EmployeePosition>();
    public ICollection<Employee> SiblingEmployees { get; } = new List<Employee>();

    public int SeatingPosition { get; private set; }
    
    public EmployeePosition( ){}
    
    public EmployeePosition(
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
    
    public void UpdateSubDepartmentId(long? subDepartmentId)
    {
        SubDepartmentId = subDepartmentId;
    }

    public void UpdateReportToPositionId(long? reportToPositionId)
    {
        ReportToPositionId = reportToPositionId;
    }

    public void UpdateSeatingPosition(int seatingPosition)
    {
        SeatingPosition = seatingPosition;
    }
}