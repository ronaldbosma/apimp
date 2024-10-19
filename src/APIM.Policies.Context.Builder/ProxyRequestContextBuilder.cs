namespace APIM.Policies.Context.Builder;

public class ProxyRequestContextBuilder
{
    public ResponseBuilder Response { get; } = new ResponseBuilder();

    public IProxyRequestContext Build()
    {
        return new ProxyRequestContext
        { 
            Response = Response.Build()
        };
    }
}
