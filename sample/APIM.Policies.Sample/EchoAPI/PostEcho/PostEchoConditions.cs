namespace APIM.Policies.Sample.EchoAPI.PostEcho;

internal class PostEchoConditions
{
    public static bool ResponseIsBadRequest(IPolicyContext policyContext)
    {
        //TODO: return context.Response.StatusCode == 400
        return true;
    }

    public static bool ResponseIsNotFound(IPolicyContext policyContext)
    {
        //TODO: return context.Response.StatusCode == 404
        return false;
    }
}
