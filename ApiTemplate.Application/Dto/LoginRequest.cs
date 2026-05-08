using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Application.Dto;

/// <summary>
/// 用户登录请求参数
/// </summary>
public record LoginRequest
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    public string Password { get; set; } = string.Empty;
}