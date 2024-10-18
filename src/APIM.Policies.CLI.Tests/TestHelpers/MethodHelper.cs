using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace APIM.Policies.CLI.Tests.TestHelpers;

internal static class MethodHelper
{
    public static async Task<MethodDeclarationSyntax> CreateMethodDeclarationSyntaxAsync(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var root = await syntaxTree.GetRootAsync();

        return root.DescendantNodes().OfType<MethodDeclarationSyntax>().Single();
    }
}
