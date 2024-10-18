namespace APIM.Policies.Sample.EchoAPI.PostEcho;

public class PostEchoRequest
{
    public static string Transform(IPolicyContext context)
    {
        return "This is the request";
    }
}
