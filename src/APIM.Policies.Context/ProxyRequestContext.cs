
namespace APIM.Policies.Context;

internal record ProxyRequestContext : IProxyRequestContext
{
    public required IResponse Response { get; init; }

    public Guid RequestId { get; init; }

    public DateTime Timestamp { get; init; }

    public IRequest Request { get; init; }

    public IApi Api { get; init; }

    public IOperation Operation { get; init; }

    public ISubscription Subscription { get; init; }

    public IUser User { get; init; }

    public IProduct Product { get; init; }

    public ProxyError LastError { get; init; }

    public IDeployment Deployment { get; init; }

    public IReadOnlyDictionary<string, object> Variables { get; init; }

    public bool Tracing { get; init; }

    public TimeSpan Elapsed { get; init; }

    public IGraphQLProperties GraphQL { get; init; }

    public void Trace(string message)
    {
        throw new NotImplementedException();
    }

    public void Trace(string source, object data)
    {
        throw new NotImplementedException();
    }
}
