namespace APIM.Policies.Context;

public class ProxyRequestContextBuilder
{
    public ResponseBuilder Response { get; private set; } = new ResponseBuilder();

    public ProxyRequestContextBuilder WithResponse(ResponseBuilder responseBuilder)
    {
        Response = responseBuilder;
        return this;
    }

    public IProxyRequestContext Build()
    {
        return new ProxyRequestContext
        { 
            Response = Response.Build()
        };
    }
}
