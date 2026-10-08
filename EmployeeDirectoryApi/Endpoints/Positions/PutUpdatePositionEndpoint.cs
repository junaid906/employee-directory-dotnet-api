using EmployeeDirectoryApi.Dtos.Positions;
using EmployeeDirectoryApi.Services.Position;

namespace EmployeeDirectoryApi.Endpoints.Positions;

public sealed class PutUpdatePositionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPut("/positions/{uniqueId:guid}",
                async (Guid uniqueId, UpdatePositionDto updatePositionDto, PositionService service, CancellationToken ct) =>
                {
                    var position = await service.UpdatePosition(uniqueId, updatePositionDto, ct);
                    return position is null ? Results.NotFound() : Results.Ok(position);
                })
            .WithName("UpdatePosition")
            .WithTags("Positions")
            .Produces<PositionDto>()
            .Produces(StatusCodes.Status404NotFound);
    }
}
