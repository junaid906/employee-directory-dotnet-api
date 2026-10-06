using EmployeeDirectoryApi.Dtos.Employees;
using EmployeeDirectoryApi.Services.Employee.Interfaces;

namespace EmployeeDirectoryApi.Endpoints.Employees;

public sealed class GetManagersSummarised : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/employees/managers-summarised", 
                async (IEmployeeService service, CancellationToken ct) => Results.Ok(await service.GetManagersSummarised(ct)))
            .WithName("GetManagersSummarised")
            .WithTags("Employees")
            .Produces<List<ManagerSummaryDto>>();
    }
}