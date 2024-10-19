namespace APIM.Policies.Sample.EchoAPI.PostEcho;

public class PostEchoResponse
{
    public static string Transform(IProxyRequestContext context)
    {
        string response = "This is the response";
        return response;
    }
}
