using ApiTemplate.Infrastructure.DynamicApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Application.IServices;

[DynamicApi("api/test-service")] // 测试自定义路由
public interface ITestService
{
    [ApiAction("GET", "{id:int}")]
    Task<string> GetByIdAsync(int id);

    [ApiAction("DELETE", "{id:int}")]
    [AllowAnonymous]
    Task DeleteAsync(int id);

    [ApiAction("GET", "message")]
    string GetMessage();

    [ApiAction("POST", "login")]
    [AllowAnonymous]
    string Login([FromQuery] string password);
}
