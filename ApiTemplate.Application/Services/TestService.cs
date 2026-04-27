using ApiTemplate.Application.IServices;
using ApiTemplate.Infrastructure.IOC;

namespace ApiTemplate.Application.Services;

[RegisterService(ServiceLifetimeType.Scoped)]
public class TestService : ITestService
{
    public string GetMessage()
    {
        return "Mock Message: 服务自动注入测试成功！";
    }
}
