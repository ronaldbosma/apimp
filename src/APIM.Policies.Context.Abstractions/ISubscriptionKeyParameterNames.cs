namespace APIM.Policies.Context.Abstractions;

public interface ISubscriptionKeyParameterNames
{
    string Header { get; }

    string Query { get; }
}