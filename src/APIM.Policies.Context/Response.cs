namespace APIM.Policies.Context;

internal record Response : IResponse
{
    public int StatusCode { get; init; } = 200;
}
