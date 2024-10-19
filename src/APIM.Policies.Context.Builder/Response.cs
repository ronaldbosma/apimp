namespace APIM.Policies.Context.Builder;

internal record Response : IResponse
{
    public int StatusCode { get; init; } = 200;
}
