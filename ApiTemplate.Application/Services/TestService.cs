using System;
using System.Threading.Tasks;
using ApiTemplate.Application.IServices;
using ApiTemplate.Infrastructure.IOC;
using ApiTemplate.Infrastructure.JWT;

namespace ApiTemplate.Application.Services;

[RegisterService(ServiceLifetimeType.Scoped)]
public class TestService : ITestService
{
    private readonly JwtHelper _jwtHelper;

    public TestService(JwtHelper jwtHelper)
    {
        _jwtHelper = jwtHelper;
    }

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

    public string Login(string password)
    {
        if (password == "woshitaozi")
        {
            return _jwtHelper.GenerateToken("admin");
        }
        
        throw new Infrastructure.Exceptions.ApiException("密码错误");
    }
}
