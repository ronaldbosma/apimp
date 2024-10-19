namespace APIM.Policies.Sample.EchoAPI.PostEcho;

public class PostEchoRequest
{
    public static string Transform(IProxyRequestContext context) => "This is the request";
}
