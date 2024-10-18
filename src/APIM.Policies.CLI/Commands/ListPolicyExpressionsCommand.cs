using APIM.Policies.CLI.Analyzers;

namespace APIM.Policies.CLI.Commands
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
