using ApiTemplate.Infrastructure.DynamicApi;
using ApiTemplate.Infrastructure.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Application.IServices;

[DynamicApi("api/test-service")] // 测试自定义路由
public interface ITestService
{
    [ApiAction("{id:int}", HttpMethodType.GET)]
    Task<string> GetByIdAsync(int id);

    [ApiAction("{id:int}", HttpMethodType.DELETE)]
    [AllowAnonymous]
    Task DeleteAsync(int id);

    [ApiAction("message", HttpMethodType.GET)]
    string GetMessage();

    [ApiAction("login")]
    [AllowAnonymous]
    string Login([FromQuery] string password);
}
