namespace APIM.Policies.CLI.Models;

internal record PolicyFile
{
    public required string Name { get; init; }
    public required string FullName { get; init; }
    public required HashSet<string> PolicyExpressionReferences { get; init; }
}
