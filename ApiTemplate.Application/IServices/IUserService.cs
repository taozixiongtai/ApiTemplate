using ApiTemplate.Application.Dto;
using ApiTemplate.Infrastructure.DynamicApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Application.IServices;

/// <summary>
/// 用户服务接口
/// </summary>
[DynamicApi("api/user")]
public interface IUserService
{
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="req">登录请求参数</param>
    /// <returns>JWT Token 字符串</returns>
    [ApiAction("login")]
    [AllowAnonymous]
    Task<string> LoginAsync([FromBody] LoginRequest req);
}