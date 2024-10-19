namespace APIM.Policies.Sample.EchoAPI.PostEcho;

internal class PostEchoConditions
{
    public static bool ResponseIsBadRequest(IPolicyContext context) => context.Response.StatusCode == 400;

    public static bool ResponseIsNotFound(IPolicyContext context) => 
        context.Response.StatusCode == 404;
}
