using System;
using System.Threading.Tasks;
using ApiTemplate.Application.IServices;
using ApiTemplate.Infrastructure.IOC;

namespace ApiTemplate.Application.Services;

[RegisterService(ServiceLifetimeType.Scoped)]
public class TestService : ITestService
{
    public Task<string> GetByIdAsync(int id)
    {
        return Task.FromResult($"获取到用户: {id}");
    }

    public Task<int> CreateAsync(CreateUserRequest req)
    {
        // 模拟创建用户并返回ID
        int newId = new Random().Next(100, 1000);
        return Task.FromResult(newId);
    }

    public Task DeleteAsync(int id)
    {
        // 模拟删除
        return Task.CompletedTask;
    }

    public string GetMessage()
    {
        return "Mock Message: 服务自动注入测试成功！";
    }
}
