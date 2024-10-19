using APIM.Policies.CLI.Extensions;
using APIM.Policies.CLI.Models;
using APIM.Policies.CLI.Tests.TestHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace APIM.Policies.CLI.Tests.Extensions;

[TestClass]
public class SyntaxTreeExtensionsTests
{
    [TestMethod]
    public async Task GetPolicyExpressionsAsync_SourceHasSinglePolicyExpression_PolicyExpressionReturned()
    {
        //Arrange
        var source = """
            namespace A.Namespace;
            internal class ClassName
            {
                public static bool MethodName(IProxyRequestContext context) => true;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var methods = await GetMethodsAsync(syntaxTree);
        var model = SyntaxHelper.CreateSemanticModel(syntaxTree);

        //Act
        var result = await syntaxTree.GetPolicyExpressionsAsync(model);

        //Assert
        var expectedResult = new Dictionary<string, PolicyExpression>
        {
            { "A.Namespace.ClassName.MethodName", methods[0].ToPolicyExpression(model) }
        };
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public async Task GetPolicyExpressionsAsync_SingleClassWithMultiplePolicyExpressions_PolicyExpressionsReturned()
    {
        //Arrange
        var source = """
            namespace A.Namespace;
            internal class ClassName
            {
                public static bool MethodName1(IProxyRequestContext context) => true;
                public static bool MethodName2(IProxyRequestContext context) => true;
                public static bool MethodName3(IProxyRequestContext context) => true;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var methods = await GetMethodsAsync(syntaxTree);
        var model = SyntaxHelper.CreateSemanticModel(syntaxTree);

        //Act
        var result = await syntaxTree.GetPolicyExpressionsAsync(model);

        //Assert
        var expectedResult = new Dictionary<string, PolicyExpression>
        {
            { "A.Namespace.ClassName.MethodName1", methods[0].ToPolicyExpression(model) },
            { "A.Namespace.ClassName.MethodName2", methods[1].ToPolicyExpression(model) },
            { "A.Namespace.ClassName.MethodName3", methods[2].ToPolicyExpression(model) }
        };
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public async Task GetPolicyExpressionsAsync_PolicyExpressionsInDifferentClasses_PolicyExpressionsReturned()
    {
        //Arrange
        var source = """
            namespace A.Namespace;
            internal class ClassName1
            {
                public static bool MethodName(IProxyRequestContext context) => true;
            }
            internal class ClassName2
            {
                public static bool MethodName(IProxyRequestContext context) => true;
            }
            internal class ClassName3
            {
                public static bool MethodName(IProxyRequestContext context) => true;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var methods = await GetMethodsAsync(syntaxTree);
        var model = SyntaxHelper.CreateSemanticModel(syntaxTree);

        //Act
        var result = await syntaxTree.GetPolicyExpressionsAsync(model);

        //Assert
        var expectedResult = new Dictionary<string, PolicyExpression>
        {
            { "A.Namespace.ClassName1.MethodName", methods[0].ToPolicyExpression(model) },
            { "A.Namespace.ClassName2.MethodName", methods[1].ToPolicyExpression(model) },
            { "A.Namespace.ClassName3.MethodName", methods[2].ToPolicyExpression(model) }
        };
        result.Should().BeEquivalentTo(expectedResult);
    }


    public static async Task<IList<MethodDeclarationSyntax>> GetMethodsAsync(SyntaxTree syntaxTree)
    {
        var root = await syntaxTree.GetRootAsync();

        return root.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();
    }
}
