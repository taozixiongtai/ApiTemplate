using ApiTemplate.Infrastructure.DynamicApi;

namespace ApiTemplate.Application.IServices;

[DynamicApi("api/test-service")]
public interface ITestService
{
    string GetMessage();
}
