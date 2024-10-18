using APIM.Policies.CLI.Extensions;
using APIM.Policies.CLI.Models;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.MSBuild;

namespace APIM.Policies.CLI.Analyzers;

internal class ProjectAnalyzer
{
    public static async Task<IDictionary<string, PolicyExpression>> GetPolicyExpressionsAsync(string source)
    {
        MSBuildLocator.RegisterDefaults();
        using var workspace = MSBuildWorkspace.Create();
        var project = await workspace.OpenProjectAsync(source);
        var compilation = await project.GetCompilationAsync() ?? throw new Exception("Unable to load project compilation");

        var result = new Dictionary<string, PolicyExpression>();

        foreach (var document in project.Documents)
        {
            var syntaxTree = await document.GetSyntaxTreeAsync() ?? throw new Exception("Unable to load syntax tree");
            var semanticModel = compilation.GetSemanticModel(syntaxTree);

            var policyExpressions = await syntaxTree.GetPolicyExpressionsAsync(semanticModel);
            foreach (var policyExpression in policyExpressions)
            {
                result[policyExpression.Key] = policyExpression.Value;
            }
        }

        return result;
    }
}
