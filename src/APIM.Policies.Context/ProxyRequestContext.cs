
namespace APIM.Policies.Context;

internal record ProxyRequestContext : IProxyRequestContext
{
    public Guid RequestId { get; init; } = Guid.NewGuid();

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;

    public required IRequest Request { get; init; }

    public required IResponse Response { get; init; }

    public IApi? Api { get; init; }

    public IOperation? Operation { get; init; }

    public ISubscription? Subscription { get; init; }

    public IUser? User { get; init; }

    public IProduct? Product { get; init; }

    public ProxyError? LastError { get; init; }

    public required IDeployment Deployment { get; init; }

    public IReadOnlyDictionary<string, object> Variables { get; init; } = new Dictionary<string, object>();

    public bool Tracing { get; init; }

    public TimeSpan Elapsed { get; init; }

    public void Trace(string message)
    {
        throw new NotImplementedException();
    }

    public void Trace(string source, object data)
    {
        throw new NotImplementedException();
    }
}
