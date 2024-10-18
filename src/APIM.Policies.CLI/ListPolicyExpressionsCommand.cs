using APIM.Policies.CLI.Extensions;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.MSBuild;

namespace APIM.Policies.CLI
{
    internal sealed class ListPolicyExpressionsCommand : AsyncCommand<ListPolicyExpressionsCommandSettings>
    {
        public override async Task<int> ExecuteAsync(CommandContext context, ListPolicyExpressionsCommandSettings settings)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(settings.Source, nameof(settings));

            AnsiConsole.MarkupLine("Load policy expressions from {0}", settings.Source);

            MSBuildLocator.RegisterDefaults();
            using var workspace = MSBuildWorkspace.Create();
            var project = await workspace.OpenProjectAsync(settings.Source);
            var compilation = await project.GetCompilationAsync() ?? throw new Exception("Unable to load project compilation");

            foreach (var document in project.Documents)
            {
                AnsiConsole.MarkupLine(document.Name);

                var syntaxTree = await document.GetSyntaxTreeAsync() ?? throw new Exception("Unable to load syntax tree");
                var semanticModel = compilation.GetSemanticModel(syntaxTree);

                var policyExpressions = await syntaxTree.GetPolicyExpressionsAsync(semanticModel);
                foreach (var policyExpression in policyExpressions)
                {
                    AnsiConsole.MarkupLine($"- {policyExpression.Key}");
                }
            }

            return 0;
        }
    }
}
