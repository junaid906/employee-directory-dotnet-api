using EmployeeDirectoryApi.Dtos.Positions;
using EmployeeDirectoryApi.Services.Position;

namespace EmployeeDirectoryApi.Endpoints.Positions;

public sealed class PostReparentPositionsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/positions/reparent",
                async (ReparentPositionsDto reparentPositionsDto, PositionService service, CancellationToken ct) =>
                {
                    await service.ReparentPositions(reparentPositionsDto, ct);
                    return Results.NoContent();
                })
            .WithName("ReparentPositions")
            .WithTags("Positions")
            .Produces(StatusCodes.Status204NoContent);
    }
}
