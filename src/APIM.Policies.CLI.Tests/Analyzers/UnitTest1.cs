using Microsoft.CodeAnalysis.CSharp;

namespace APIM.Policies.CLI.Tests.Analyzers;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        // Arrange
        var source = """
            namespace A.Namespace;
            internal class ClassName
            {
                public static bool MethodName(IPolicyContext policyContext)
                {
                    return true;
                }
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        var expectedResult = new Dictionary<string, string>
        {
            { "A.Namespace.ClassName.MethodName", "return true;" }
        };

        // Act
        var result = new Dictionary<string, string>();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}