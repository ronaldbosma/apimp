using APIM.Policies.CLI.Models;
using APIM.Policies.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace APIM.Policies.CLI.Extensions;

internal static class MethodDeclarationSyntaxExtensions
{
    public static PolicyExpression ToPolicyExpression(this MethodDeclarationSyntax method, SemanticModel model)
    {
        if (!method.IsPolicyExpression(model))
        {
            throw new ArgumentException("Method is not a policy expression", nameof(method));
        }

        return new PolicyExpression
        {
            FullName = method.GetFullName(model),
            Body = method.GetBody(),
            IsSingleStatement = method.ExpressionBody != null
        };
    }

    public static bool IsPolicyExpression(this MethodDeclarationSyntax method, SemanticModel model)
    {
        if (method.ParameterList.Parameters.Count != 1 ||
            method.ParameterList.Parameters[0].Identifier.ValueText != "context")
        {
            return false;
        }

        var parameterType = method.ParameterList.Parameters[0].Type;
        if (parameterType == null)
        {
            return false;
        }

        var parameterSymbloInfo = model.GetSymbolInfo(parameterType);
        return parameterSymbloInfo.Symbol?.ToDisplayString() == typeof(IPolicyContext).FullName;
    }

    public static string GetFullName(this MethodDeclarationSyntax method, SemanticModel model)
    {
        var methodSymbol = model.GetDeclaredSymbol(method);
        if (methodSymbol != null)
        {
            var fullName = $"{methodSymbol.ContainingType.Name}.{methodSymbol.Name}";

            var containingNamespace = methodSymbol.ContainingType.ContainingNamespace;
            if (!containingNamespace.IsGlobalNamespace)
            {
                fullName = $"{containingNamespace}.{fullName}";
            }

            return fullName;
        }

        throw new InvalidOperationException($"Unable to get full name of method {method.Identifier}");
    }

    public static string GetBody(this MethodDeclarationSyntax method)
    {
        if (method.Body != null)
        {
            return method.Body.ToFullString();
        }
        else if (method.ExpressionBody != null)
        {
            return method.ExpressionBody.Expression.ToFullString();
        }

        return string.Empty;
    }
}
