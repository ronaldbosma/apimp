namespace APIM.Policies.Context.Abstractions;

public interface IApi
{
    string Id { get; }

    string Name { get; }

    string Path { get; }

    IUrl ServiceUrl { get; }

    IEnumerable<string> Protocols { get; }

    ISubscriptionKeyParameterNames SubscriptionKeyParameterNames { get; }
}