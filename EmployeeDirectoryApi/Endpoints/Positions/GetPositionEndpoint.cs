using EmployeeDirectoryApi.Dtos.Positions;
using EmployeeDirectoryApi.Services.Position;

namespace EmployeeDirectoryApi.Endpoints.Positions;

public sealed class GetPositionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/positions/{uniqueId:guid}", async (
                Guid uniqueId, PositionService service, CancellationToken ct) =>
            {
                var position = await service.GetPosition(uniqueId, ct);
                return position is null ? Results.NotFound() : Results.Ok(position);
            })
            .WithName("GetPosition")
            .WithTags("Positions")
            .Produces<PositionDto>()
            .Produces(StatusCodes.Status404NotFound);
    }
}
