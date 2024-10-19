using APIM.Policies.CLI.Extensions;
using APIM.Policies.CLI.Models;
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
                public static bool MethodName(IProxyRequestContext context)
                {
                    return true;
                }
            }
            """;
        var method = await SyntaxHelper.CreateMethodDeclarationSyntaxAsync(source);

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
                public static bool MethodName(IProxyRequestContext context)
                {
                    var result = true;
                    // A comment
                    return result;
                }
            }
            """;
        var method = await SyntaxHelper.CreateMethodDeclarationSyntaxAsync(source);

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
                public static bool MethodName(IProxyRequestContext context) => true;
            }
            """;
        var method = await SyntaxHelper.CreateMethodDeclarationSyntaxAsync(source);

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
                    public static bool MethodName(IProxyRequestContext context) => true;
                }
            }
            """;

        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

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
                public static bool MethodName(IProxyRequestContext context) => true;
            }
            """;

        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

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
                public static bool MethodName(IProxyRequestContext context) => true;
            }
            """;

        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.GetFullName(model);

        //Assert
        result.Should().Be("ClassName.MethodName");
    }

    [TestMethod]
    public async Task IsPolicyExpression_MethodHasOneArgumentOfTypeIProxyRequestContextAndNameContext_TrueReturned()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName(IProxyRequestContext context) => return true;
            }
            """;
        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.IsPolicyExpression(model);

        //Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public async Task IsPolicyExpression_MethodHasContextArgumentWithFullTypeName_TrueReturned()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName(APIM.Policies.Context.IProxyRequestContext context) => return true;
            }
            """;
        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.IsPolicyExpression(model);

        //Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public async Task IsPolicyExpression_MethodHasOneArgumentOfTypeIProxyRequestContextButNameIsNotContext_TrueReturned()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName(IProxyRequestContext notContext) => return true;
            }
            """;
        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.IsPolicyExpression(model);

        //Assert
        result.Should().BeFalse();
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
        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

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
        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.IsPolicyExpression(model);

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public async Task IsPolicyExpression_MethodReturnsVoid_FalseReturned()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static void MethodName(IProxyRequestContext context) {}
            }
            """;
        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.IsPolicyExpression(model);

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public async Task ToPolicyExpression_MethodWithExpressionBody_PolicyExpressionReturned()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName(IProxyRequestContext context) => true;
            }
            """;

        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.ToPolicyExpression(model);

        //Assert
        var expectedResult = new PolicyExpression
        {
            FullName = "ClassName.MethodName",
            Body = "true",
            IsSingleStatement = true
        };
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public async Task ToPolicyExpression_MethodWithBody_PolicyExpressionReturned()
    {
        //Arrange
        var source = """
            namespace A.Namespace;
            internal class ClassName
            {
                public static bool MethodName(IProxyRequestContext context)
                {
                    return true;
                }
            }
            """;

        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var result = syntax.ToPolicyExpression(model);

        //Assert
        var expectedResult = new PolicyExpression
        {
            FullName = "A.Namespace.ClassName.MethodName",
            Body = """
                    {
                        return true;
                    }

                """,
            IsSingleStatement = false
        };
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public async Task ToPolicyExpression_MethodIsNotPolicyExpression_ArgumentExceptionThrown()
    {
        //Arrange
        var source = """
            internal class ClassName
            {
                public static bool MethodName() => return false;
            }
            """;

        var (syntax, model) = await SyntaxHelper.CreateMethodDeclarationSyntaxAndSemanticModelAsync(source);

        //Act
        var act = () => syntax.ToPolicyExpression(model);

        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Method is not a policy expression (Parameter 'method')");
    }
}
