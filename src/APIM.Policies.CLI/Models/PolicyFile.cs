namespace APIM.Policies.CLI.Models;

internal record PolicyFile
{
    public required string Name { get; init; }
    public required string FullName { get; init; }
    public required string Content { get; init; }
    public required HashSet<string> PolicyExpressionReferences { get; init; }

    public string MergePolicyExpressions(IDictionary<string, PolicyExpression> policyExpressions)
    {
        var generatedContent = Content;

        foreach (var reference in PolicyExpressionReferences)
        {
            var policyExpression = policyExpressions[reference];
            generatedContent = generatedContent.Replace($"@method:{reference}", policyExpression.GetEnclosedBody());
        }

        return generatedContent;
    }
}
