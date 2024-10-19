using APIM.Policies.Context;
using APIM.Policies.Context.Abstractions;
using APIM.Policies.Sample.EchoAPI.GetEcho;
using FluentAssertions;

namespace APIM.Policies.Sample.Tests.EchoAPI.GetEcho;

[TestClass]
public class ExpressionsTests
{
    [DataTestMethod]
    [DataRow(0)]
    [DataRow(200)]
    [DataRow(300)]
    [DataRow(399)]
    public void ResponseIsError_StatusCodeIsSmallerThan400_FalseReturned(int statusCode)
    {
        //Arrange
        var builder = new ProxyRequestContextBuilder();
        builder.Response.WithStatusCode(statusCode);

        IProxyRequestContext context = builder.Build();

        //Act
        bool result = Expressions.ResponseIsError(context);

        //Assert
        result.Should().BeFalse();
    }

    [DataTestMethod]
    [DataRow(400)]
    [DataRow(404)]
    [DataRow(500)]
    public void ResponseIsError_StatusCodeIs400OrLarger_TrueReturned(int statusCode)
    {
        //Arrange
        var responseBuilder = new ResponseBuilder().WithStatusCode(statusCode);
        var context = new ProxyRequestContextBuilder().WithResponse(responseBuilder).Build();

        //Act
        bool result = Expressions.ResponseIsError(context);

        //Assert
        result.Should().BeTrue();
    }
}