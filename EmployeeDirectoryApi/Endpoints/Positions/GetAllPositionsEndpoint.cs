using EmployeeDirectoryApi.Dtos.Positions;
using EmployeeDirectoryApi.Services.Position.Interfaces;

namespace EmployeeDirectoryApi.Endpoints.Positions;

public sealed class GetAllPositionsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/positions",
                async (IPositionService service, CancellationToken ct) => Results.Ok(await service.GetAllPositions(ct)))
            .WithName("GetAllPositions")
            .WithTags("Positions")
            .Produces<List<PositionDto>>();
    }
}
