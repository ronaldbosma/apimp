namespace APIM.Policies.Context;

internal record ProxyRequestContext : IProxyRequestContext
{
    public required IResponse Response { get; init; }
}
