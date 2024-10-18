using APIM.Policies.CLI.Analyzers;
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

            var policyExpressions = await ProjectAnalyzer.GetPolicyExpressionsAsync(settings.Source);
            foreach (var policyExpression in policyExpressions)
            {
                AnsiConsole.MarkupLine($"- {policyExpression.Key}");
            }

            return 0;
        }
    }
}
