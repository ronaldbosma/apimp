using APIM.Policies.Core;
using System.Reflection;

namespace APIM.Policies.CLI
{
    internal sealed class ListPolicyExpressionsCommand : Command<ListPolicyExpressionsCommandSettings>
    {
        public override int Execute(CommandContext context, ListPolicyExpressionsCommandSettings settings)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(settings.Source, nameof(settings));

            AnsiConsole.MarkupLine("Load policy expressions from {0}", settings.Source);


            return 0;
        }
    }
}
