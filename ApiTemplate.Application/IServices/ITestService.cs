using System;
using System.Threading.Tasks;
using ApiTemplate.Infrastructure.DynamicApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Application.IServices;

public class CreateUserRequest
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}

[DynamicApi("api/test-service")] // 测试自定义路由
public interface ITestService
{
    [ApiAction("GET", "{id:int}")]
    Task<string> GetByIdAsync(int id);

    [ApiAction("POST", "create")]
    Task<int> CreateAsync([FromBody] CreateUserRequest req);

    [ApiAction("DELETE", "{id:int}")]
    [AllowAnonymous]
    Task DeleteAsync(int id);

    [ApiAction("GET", "message")]
    string GetMessage();
}
