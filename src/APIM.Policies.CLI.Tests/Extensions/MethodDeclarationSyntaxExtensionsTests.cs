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
                public static bool MethodName(IPolicyContext context)
                {
                    return true;
                }
            }
            """;
        var method = await MethodHelper.CreateMethodDeclarationSyntaxAsync(source);

        //Act
        var result = method.GetBody();

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
                public static bool MethodName(IPolicyContext context)
                {
                    var result = true;
                    // A comment
                    return result;
                }
            }
            """;
        var method = await MethodHelper.CreateMethodDeclarationSyntaxAsync(source);

        //Act
        var result = method.GetBody();

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
                public static bool MethodName(IPolicyContext context) => true;
            }
            """;
        var method = await MethodHelper.CreateMethodDeclarationSyntaxAsync(source);

        //Act
        var result = method.GetBody();

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
                    public static bool MethodName(IPolicyContext context) => true;
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
                public static bool MethodName(IPolicyContext context) => true;
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
                public static bool MethodName(IPolicyContext context) => true;
            }
            """;

        var (syntax, model) = await MethodHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.GetFullName(model);

        //Assert
        result.Should().Be("ClassName.MethodName");
    }

    [TestMethod]
    public async Task IsPolicyExpression_MethodHasOneArgumentOfTypeIPolicyContextAndNameContext_TrueReturned()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName(IPolicyContext context) => return true;
            }
            """;
        var (syntax, model) = await MethodHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.IsPolicyExpression(model);

        //Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public async Task IsPolicyExpression_MethodHasPolicyContextArgumentWithFullname_TrueReturned()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName(APIM.Policies.Core.IPolicyContext context) => return true;
            }
            """;
        var (syntax, model) = await MethodHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.IsPolicyExpression(model);

        //Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public async Task IsPolicyExpression_MethodHasNoArguments_FalseReturned()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName() => return true;
            }
            """;
        var (syntax, model) = await MethodHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.IsPolicyExpression(model);

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public async Task IsPolicyExpression_MethodHasArgumentWithWrongType_FalseReturned()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName(string context) => return true;
            }
            """;
        var (syntax, model) = await MethodHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.IsPolicyExpression(model);

        //Assert
        result.Should().BeFalse();
    }
}
