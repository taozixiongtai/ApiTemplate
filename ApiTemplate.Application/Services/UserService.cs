using ApiTemplate.Application.Dto;
using ApiTemplate.Application.IServices;
using ApiTemplate.Domain.Models;
using ApiTemplate.Infrastructure.Check;
using ApiTemplate.Infrastructure.Exceptions;
using ApiTemplate.Infrastructure.IOC;
using ApiTemplate.Infrastructure.JWT;

namespace ApiTemplate.Application.Services;

[RegisterService(ServiceLifetimeType.Scoped)]
public class UserService(IBaseRepository<User> userRepo, JwtHelper jwtHelper) : IUserService
{

    public async Task<string> LoginAsync(LoginRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
        {
            throw new ApiException("用户名或密码不能为空");
        }

        //  根据用户名和密码查询
        var user = await userRepo.GetFirstAsync(u => u.Username == req.Username && u.Password == req.Password);
        Check.IsNull(user, "用户名或密码错误");

        // 生成并返回 JWT Token
        return jwtHelper.GenerateToken(user.Username);
    }
}