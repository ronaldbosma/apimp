using APIM.Policies.CLI.Analyzers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIM.Policies.CLI.Commands;

internal class MergeCommand : AsyncCommand<MergeCommandSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, MergeCommandSettings settings)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(settings.Project, nameof(settings));

        var target = settings.Target;
        if (string.IsNullOrWhiteSpace(target))
        {
            target = Path.GetDirectoryName(settings.Project);
        }

        var policyExpressions = await ProjectAnalyzer.GetPolicyExpressionsAsync(settings.Project);
        var policyFiles = await PolicyFilesAnalyzer.GetPolicyFilesWithExpressionReferences(target!, settings.Include!, settings.Exclude);

        foreach (var policyFile in policyFiles)
        {
            foreach (var reference in policyFile.PolicyExpressionReferences)
            {
                var policyExpression = policyExpressions[reference];
            }
        }

        return 0;
    }
}
