namespace APIM.Policies.Sample.EchoAPI.PostEcho;

internal class PostEchoConditions
{
    public static bool ResponseIsBadRequest(IProxyRequestContext context) => context.Response.StatusCode == 400;

    public static bool ResponseIsNotFound(IProxyRequestContext context) => 
        context.Response.StatusCode == 404;
}
