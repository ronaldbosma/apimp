# API Management Policy Dev Tools

This project aims to provide a set of tools to help developers work with Azure API Management policies. This project is still a **work in progress**. 

API Management provides the ability to use C# [policy expressions](https://learn.microsoft.com/en-us/azure/api-management/api-management-policy-expressions) inside your policies. 
This is a powerful feature that allows you to create dynamic policies that can be used to transform requests and responses. 
The problem is that the policy expressions are not easy to work with, because you define them inside XML files. 

This project gives developers the ability to code there policy expressions in a .NET project and merge these into XML policy files that can be imported into API Management.

## Example

The following example shows a policy snippet with a single statement expressions in the condition and a multi-statement expression in the set-body element.

```xml
<choose>
    <when condition="@(return context.Response.StatusCode >= 400)">
        <set-body>Error</set-body>
    </when>
    <otherwise>
        <set-body>@{
            string response = "This is the response";
            return response;
        }
        </set-body>
    </otherwise>
</choose>
```

Because the expressions are defined in the XML file, they're difficult to unit test and you don't have intelligence or syntax highlighting.

To extract the expressions into a .NET project, you would create a class with static methods that represent the expressions. 
The class would look like this:

```csharp
namespace APIM.Policies.Sample.EchoAPI.GetEcho;

internal class Expressions
{
    public static bool ResponseIsError(IPolicyContext context) => context.Response.StatusCode >= 400;

    public static string TransformResponse(IPolicyContext context)
    {
        string response = "This is the response";
        return response;
    }
}
```

Each policy expression method must:
- have a single parameter of type `IPolicyContext` with name `context`
- have a return type
- be static

Methods that define an expression using the `=>` syntax will be treated as a single statement expression. 
Methods that use the `{}` syntax will be treated as multi-statement expressions.

The XML policy file can then be updated to reference the expressions like this:

```xml
<choose>
    <when condition="@expression:APIM.Policies.Sample.EchoAPI.GetEcho.Expressions.ResponseIsError">
        <set-body>Error</set-body>
    </when>
    <otherwise>
        <set-body>@expression:APIM.Policies.Sample.EchoAPI.GetEcho.Expressions.TransformResponse</set-body>
    </otherwise>
</choose>
```

The convention `<namespace>.<class>.<method>` is used to reference a specific policy expression method. 
The `@expression:` prefix is used to indicate that it's a policy expression reference.

The merge tool will replace the policy expression references with the actual method bodies. 
A separate XML file will be generated with a name following the convention `<name>.generated.<extension>`.
