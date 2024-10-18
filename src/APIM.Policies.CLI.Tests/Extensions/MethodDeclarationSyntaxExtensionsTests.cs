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
            public static bool MethodName(IPolicyContext policyContext)
            {
                return true;
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
            public static bool MethodName(IPolicyContext policyContext)
            {
                var result = true;
                // A comment
                return result;
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
        var source = "public static bool MethodName(IPolicyContext policyContext) => true;";
        var method = await MethodHelper.CreateMethodDeclarationSyntaxAsync(source);

        //Act
        var result = method.GetBody();

        //Assert
        result.Should().Be("true");
    }
}
