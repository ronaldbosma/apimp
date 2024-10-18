using APIM.Policies.Core;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System.Reflection;

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

            foreach (var document in project.Documents)
            {
                AnsiConsole.MarkupLine(document.Name);

                var syntaxTree = await document.GetSyntaxTreeAsync() ?? throw new Exception("Unable to load syntax tree");
                var root = await syntaxTree.GetRootAsync();

                var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
                foreach (var method in methods)
                {
                    AnsiConsole.MarkupLine($"- {method.Identifier.Text}");
                }
            }

            return 0;
        }
    }
}
