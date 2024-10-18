using Microsoft.CodeAnalysis;
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

    public static async Task<(MethodDeclarationSyntax syntax, SemanticModel model)> CreateMethodDeclarationSyntaxAndSemanticModelAsync(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        var root = await syntaxTree.GetRootAsync();
        var method = root.DescendantNodes().OfType<MethodDeclarationSyntax>().Single();

        var compilation = CSharpCompilation.Create("TestCompilation", [syntaxTree]);
        var model = compilation.GetSemanticModel(syntaxTree);

        return (method, model);
    }
}
