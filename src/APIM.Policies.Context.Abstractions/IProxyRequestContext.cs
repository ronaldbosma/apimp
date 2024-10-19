namespace APIM.Policies.Context.Abstractions
{
    public interface IProxyRequestContext
    {
        Guid RequestId { get; }

        DateTime Timestamp { get; }

        IRequest Request { get; }

        IResponse Response { get; }

        IApi? Api { get; }

        IOperation? Operation { get; }

        ISubscription? Subscription { get; }

        IUser? User { get; }

        IProduct? Product { get; }

        ProxyError? LastError { get; }

        IDeployment Deployment { get; }

        IReadOnlyDictionary<string, object> Variables { get; }

        bool Tracing { get; }

        TimeSpan Elapsed { get; }

        //TODO: check how to handle this property. Raises an exception in REST API's
        //IGraphQLProperties GraphQL { get; }

        void Trace(string message);

        void Trace(string source, object data);
    }
}
