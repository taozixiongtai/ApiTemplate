using ApiTemplate.Infrastructure.Enum;

namespace ApiTemplate.Infrastructure.DynamicApi;

[AttributeUsage(AttributeTargets.Method)]
public class ApiActionAttribute : Attribute
{
    public HttpMethodType HttpMethod { get; }
    public string Route { get; }

    public ApiActionAttribute(string route = "", HttpMethodType httpMethod = HttpMethodType.POST)
    {
        HttpMethod = httpMethod;
        Route = route;
    }
}
