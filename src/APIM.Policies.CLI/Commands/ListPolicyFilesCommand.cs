using APIM.Policies.CLI.Analyzers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIM.Policies.CLI.Commands;

[Description("Lists policy files that have policy expression references")]
internal class ListPolicyFilesCommand : AsyncCommand<ListPolicyFilesCommandSettings>
{
    public override Task<int> ExecuteAsync(CommandContext context, ListPolicyFilesCommandSettings settings)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(settings.Folder, nameof(settings));

        AnsiConsole.MarkupLine("Load policy files with policy expression references from {0}", settings.Folder);


        return Task.FromResult(0);
    }
}
