using APIM.Policies.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace APIM.Policies.CLI.Tests.TestHelpers;

internal static class SyntaxHelper
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

        var model = CreateSemanticModel(syntaxTree);

        return (method, model);
    }

    public static SemanticModel CreateSemanticModel(SyntaxTree syntaxTree)
    {
        // Add an implicit using for APIM.Policies.Core so we can use IPolicyContext in code snippets without having to specify the name
        var implicitUsings = CSharpSyntaxTree.ParseText("global using APIM.Policies.Core;");

        // We load the assembly containing IPolicyContext so we can reference it in our code snippets.
        var metadataReference = MetadataReference.CreateFromFile(typeof(IPolicyContext).Assembly.Location);

        // Create the semantic model using the source syntax tree, implicint usings and assembly with IPolicyContext
        var compilation = CSharpCompilation.Create("TestCompilation", [syntaxTree, implicitUsings], [metadataReference]);
        return compilation.GetSemanticModel(syntaxTree);
    }
}
