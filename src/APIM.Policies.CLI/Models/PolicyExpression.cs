namespace APIM.Policies.CLI.Models;

internal record PolicyExpression
{
    public required string FullName { get; init; }
    public required string Body { get; init; }
    public required bool IsSingleStatement { get; init; }

    public string GetEnclosedBody()
    {
        return IsSingleStatement
            ? $"@({Body})"
            : $"@{Body.TrimStart()}";
    }
}
