namespace APIM.Policies.Sample.EchoAPI.GetEcho;

internal class Expressions
{
    public static bool ResponseIsError(IProxyRequestContext context) => context.Response.StatusCode >= 400;

    public static string TransformResponse(IProxyRequestContext context)
    {
        string response = "This is the response";
        return response;
    }
}
