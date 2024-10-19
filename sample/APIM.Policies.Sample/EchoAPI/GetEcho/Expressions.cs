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
