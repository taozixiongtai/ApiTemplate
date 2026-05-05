using ApiTemplate.Application.Dto;
using ApiTemplate.Application.IServices;
using ApiTemplate.Domain.Models;
using ApiTemplate.Infrastructure.Check;
using ApiTemplate.Infrastructure.Exceptions;
using ApiTemplate.Infrastructure.IOC;
using ApiTemplate.Infrastructure.JWT;
using Check = ApiTemplate.Infrastructure.Check.Check;

namespace ApiTemplate.Application.Services;

/// <summary>
/// 用户服务实现类
/// </summary>
[RegisterService(ServiceLifetimeType.Scoped)]
public class UserService(IBaseRepository<User> userRepo, JwtHelper jwtHelper) : IUserService
{
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="req">登录请求参数</param>
    /// <returns>JWT Token 字符串</returns>
    public async Task<string> LoginAsync(LoginRequest req)
    {
        Check.NotEmpty(req.Username, "用户名不能为空");
        Check.NotEmpty(req.Password, "密码不能为空");

        //  根据用户名和密码查询
        var user = await userRepo.GetFirstAsync(u => u.Username == req.Username && u.Password == req.Password);
        Check.NotNull(user, "用户名或密码错误");

        // 生成并返回 JWT Token
        return jwtHelper.GenerateToken(user.Username);
    }
}