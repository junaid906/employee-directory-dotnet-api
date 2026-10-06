namespace EmployeeDirectoryApi.Endpoints.SubDepartments;

public static class SubDepartmentEndpoints
{
    public static IEndpointRouteBuilder MapSubDepartments(this IEndpointRouteBuilder endpoint)
    {
        new GetAllSubDepartmentsEndpoint().MapEndpoint(endpoint);

        return endpoint;
    }
}
