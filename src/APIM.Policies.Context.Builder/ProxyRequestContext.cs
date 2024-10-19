namespace APIM.Policies.Context.Builder;

internal record ProxyRequestContext : IProxyRequestContext
{
    public required IResponse Response { get; init; }
}
