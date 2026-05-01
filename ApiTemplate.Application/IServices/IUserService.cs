using ApiTemplate.Application.Dto;
using ApiTemplate.Infrastructure.DynamicApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Application.IServices;

[DynamicApi("api/user")]
public interface IUserService
{
    [ApiAction("POST", "login")]
    [AllowAnonymous]
    Task<string> LoginAsync([FromBody] LoginRequest req);
}