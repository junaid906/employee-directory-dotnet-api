using EmployeeDirectoryApi.Dtos.Positions;
using EmployeeDirectoryApi.Services.Position;

namespace EmployeeDirectoryApi.Endpoints.Positions;

public sealed class PostInsertPositionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/positions/insert-between",
                async (InsertPositionDto insertPositionDto, PositionService service, CancellationToken ct) =>
                {
                    var position = await service.InsertPosition(insertPositionDto, ct);
                    return Results.Created($"/positions/{position.UniqueId}", position);
                })
            .WithName("InsertPosition")
            .WithTags("Positions")
            .Produces<PositionDto>(StatusCodes.Status201Created);
    }
}
