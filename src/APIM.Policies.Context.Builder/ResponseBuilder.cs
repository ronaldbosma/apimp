namespace APIM.Policies.Context.Builder;

public class ResponseBuilder
{
    private Response _response = new Response();

    public ResponseBuilder WithStatusCode(int statusCode)
    {
        _response = _response with { StatusCode = statusCode };
        return this;
    }

    public IResponse Build()
    {
        return _response;
    }
}
