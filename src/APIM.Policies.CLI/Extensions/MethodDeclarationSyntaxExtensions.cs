using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace APIM.Policies.CLI.Extensions;

internal static class MethodDeclarationSyntaxExtensions
{
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
