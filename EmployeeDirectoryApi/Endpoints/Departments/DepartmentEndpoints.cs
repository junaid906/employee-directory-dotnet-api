namespace EmployeeDirectoryApi.Endpoints.Departments;

public static class DepartmentEndpoints
{
    public static IEndpointRouteBuilder MapDepartments(this IEndpointRouteBuilder endpoint)
    {
        new GetAllDepartmentsEndpoint().MapEndpoint(endpoint);

        return endpoint;
    }
}
