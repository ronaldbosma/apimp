using APIM.Policies.CLI.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace APIM.Policies.CLI.Extensions;

internal static class SyntaxTreeExtensions
{
    public static async Task<IDictionary<string, PolicyExpression>> GetPolicyExpressionsAsync(this SyntaxTree syntaxTree, SemanticModel model)
    {
        var root = await syntaxTree.GetRootAsync();
        var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

        var policyExpressions = new Dictionary<string, PolicyExpression>();

        foreach (var method in methods.Where(m => m.IsPolicyExpression(model)))
        {
            var policyExpression = method.ToPolicyExpression(model);
            policyExpressions.Add(policyExpression.FullName, policyExpression);
        }

        return policyExpressions;
    }
}
