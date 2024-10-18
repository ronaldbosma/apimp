using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIM.Policies.CLI
{
    internal sealed class ListPolicyExpressionsCommand : Command<ListPolicyExpressionsCommandSettings>
    {
        public override int Execute(CommandContext context, ListPolicyExpressionsCommandSettings settings)
        {
            AnsiConsole.Markup("Policy expressions");
            return 0;
        }
    }
}
