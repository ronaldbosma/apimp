using APIM.Policies.CLI.Extensions;
using APIM.Policies.CLI.Tests.TestHelpers;

namespace APIM.Policies.CLI.Tests.Extensions;

[TestClass]
public class MethodDeclarationSyntaxExtensionsTests
{
    [TestMethod]
    public async Task GetBody_MethodHasBodyWithSingleLine_BodyReturn()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName(IPolicyContext policyContext)
                {
                    return true;
                }
            }
            """;
        var methodSyntax = await MethodHelper.CreateMethodDeclarationSyntaxAsync(source);

        //Act
        var result = methodSyntax.GetBody();

        //Assert
        var expectedResult = """
                {
                    return true;
                }

            """;
        result.Should().Be(expectedResult);
    }

    [TestMethod]
    public async Task GetBody_MethodHasBodyWithMultipleLines_BodyReturn()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName(IPolicyContext policyContext)
                {
                    var result = true;
                    // A comment
                    return result;
                }
            }
            """;
        var methodSyntax = await MethodHelper.CreateMethodDeclarationSyntaxAsync(source);

        //Act
        var result = methodSyntax.GetBody();

        //Assert
        var expectedResult = """
                {
                    var result = true;
                    // A comment
                    return result;
                }

            """;
        result.Should().Be(expectedResult);
    }

    [TestMethod]
    public async Task GetBody_MethodsHasSimpleBodyExpression_ExpressionReturned()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName(IPolicyContext policyContext) => true;
            }
            """;
        var methodSyntax = await MethodHelper.CreateMethodDeclarationSyntaxAsync(source);

        //Act
        var result = methodSyntax.GetBody();

        //Assert
        result.Should().Be("true");
    }

    [TestMethod]
    public async Task GetFullName_MethodInClassInNamespace_FullNameWithNamespaceAndClassAndMethodReturned()
    {
        //Arrange
        var source = """
            namespace A.Namespace
            {
                internal class ClassName
                {
                    public static bool MethodName(IPolicyContext policyContext) => true;
                }
            }
            """;

        var (syntax, model) = await MethodHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.GetFullName(model);

        //Assert
        result.Should().Be("A.Namespace.ClassName.MethodName");
    }

    [TestMethod]
    public async Task GetFullName_FileScopedNamespace_FullNameWithNamespaceAndClassAndMethodReturned()
    {
        //Arrange
        var source = """
            namespace A.Namespace;
            internal class ClassName
            {
                public static bool MethodName(IPolicyContext policyContext) => true;
            }
            """;

        var (syntax, model) = await MethodHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.GetFullName(model);

        //Assert
        result.Should().Be("A.Namespace.ClassName.MethodName");
    }

    [TestMethod]
    public async Task GetFullName_NoNamespace_FullNameWithClassAndMethodReturned()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName(IPolicyContext policyContext) => true;
            }
            """;

        var (syntax, model) = await MethodHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.GetFullName(model);

        //Assert
        result.Should().Be("ClassName.MethodName");
    }
}
