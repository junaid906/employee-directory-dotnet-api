using EmployeeDirectoryApi.Dtos.Employees;
using EmployeeDirectoryApi.Services.Employee;

namespace EmployeeDirectoryApi.Endpoints.Employees;

public sealed class GetManagersSummarisedEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/employees/managers-summarised", 
                async (EmployeeService service, CancellationToken ct) => Results.Ok(await service.GetManagersSummarised(ct)))
            .WithName("GetManagersSummarised")
            .WithTags("Employees")
            .Produces<List<ManagerSummaryDto>>();
    }
}