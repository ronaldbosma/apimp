namespace APIM.Policies.Context.Abstractions;

public interface IUrl
{
    string Scheme { get; }

    string Host { get; }

    int Port { get; }

    string Path { get; }

    IReadOnlyDictionary<string, string[]> Query { get; }

    string QueryString { get; }
}