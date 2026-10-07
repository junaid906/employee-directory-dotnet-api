using EmployeeDirectoryApi.Dtos.Positions;
using EmployeeDirectoryApi.Services.Position.Interfaces;

namespace EmployeeDirectoryApi.Endpoints.Positions;

public sealed class CreatePositionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/positions",
                async (CreatePositionDto createPositionDto, IPositionService service, CancellationToken ct) =>
                {
                    var position = await service.CreatePosition(createPositionDto, ct);
                    return Results.Created($"/positions/{position.UniqueId}", position);
                })
            .WithName("CreatePosition")
            .WithTags("Positions")
            .Produces<PositionDto>(StatusCodes.Status201Created);
    }
}
