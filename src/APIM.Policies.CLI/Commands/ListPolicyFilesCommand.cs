using APIM.Policies.CLI.Analyzers;
using System.ComponentModel;

namespace APIM.Policies.CLI.Commands;

[Description("Lists policy files that have policy expression references")]
internal class ListPolicyFilesCommand : AsyncCommand<ListPolicyFilesCommandSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, ListPolicyFilesCommandSettings settings)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(settings.Folder, nameof(settings));

        AnsiConsole.WriteLine("Load policy files with policy expression references from {0}", settings.Folder);

        var policyFiles = await PolicyFilesAnalyzer.GetPolicyFilesWithExpressionReferences(settings.Folder, settings.Include!, settings.Exclude);

        foreach (var policyFile in policyFiles)
        {
            AnsiConsole.WriteLine(policyFile.FullName);
            foreach (var reference in policyFile.PolicyExpressionReferences)
            {
                AnsiConsole.WriteLine("- {0}", reference);
            }
        }

        return 0;
    }
}
