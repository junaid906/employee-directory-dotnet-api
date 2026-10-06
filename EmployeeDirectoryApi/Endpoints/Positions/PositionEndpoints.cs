namespace EmployeeDirectoryApi.Endpoints.Positions;

public static class PositionEndpoints
{
    public static IEndpointRouteBuilder MapPositions(this IEndpointRouteBuilder endpoint)
    {
        new GetAllPositionsEndpoint().MapEndpoint(endpoint);
        new GetPositionEndpoint().MapEndpoint(endpoint);
        new CreatePositionEndpoint().MapEndpoint(endpoint);

        return endpoint;
    }
}
