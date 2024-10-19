namespace APIM.Policies.Context;

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
