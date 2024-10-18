using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace APIM.Policies.CLI.Extensions;

internal static class MethodDeclarationSyntaxExtensions
{
    public static string GetBody(this MethodDeclarationSyntax method)
    {
        if (method.Body != null)
        {
            // We want the 'inner' body without the surrounding { and }.
            // So, we're not using method.Body.ToFullString(), but looping over statements of the body.
            var statements = method.Body.Statements;

            var methodBodyWithoutBraces = new StringBuilder();
            foreach (var statement in statements)
            {
                methodBodyWithoutBraces.Append(statement.ToFullString());
            }
            
            var body = methodBodyWithoutBraces.ToString();

            // If the body ends with a carriage return and line feed, remove them
            if (body.EndsWith("\r\n"))
            {
                body = body.Remove(body.Length - "\r\n".Length);
            }

            return body;
        }
        else if (method.ExpressionBody != null)
        {
            return method.ExpressionBody.Expression.ToFullString();
        }

        return string.Empty;
    }
}
