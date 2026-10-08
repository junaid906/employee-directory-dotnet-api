namespace EmployeeDirectoryApi.Exceptions;

public class PositionCycleException : Exception
{
    public PositionCycleException(string message) : base(message)
    {
    }
}
