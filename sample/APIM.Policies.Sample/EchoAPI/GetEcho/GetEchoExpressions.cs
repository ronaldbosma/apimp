namespace APIM.Policies.Sample.EchoAPI.GetEcho;

internal class GetEchoExpressions
{
    //TODO: return context.Response.StatusCode >= 400
    public static bool ResponseIsError(IPolicyContext context) => true;

    public static string TransformResponse(IPolicyContext context)
    {
        string response = "This is the response";
        return response;
    }
}
