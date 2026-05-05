using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ApiTemplate.Infrastructure.IOC;

namespace ApiTemplate.Infrastructure.JWT;

[RegisterService(ServiceLifetimeType.Singleton)]
public class JwtHelper(IConfiguration configuration)
{
    public string GenerateToken(string username)
    {
        // 1. 获取配置
        var secretKey = configuration["Jwt:SecretKey"] ?? "default_super_secret_key_needs_to_be_long_enough";
        var issuer = configuration["Jwt:Issuer"] ?? "ApiTemplate";
        var audience = configuration["Jwt:Audience"] ?? "ApiTemplateClient";
        var expireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"] ?? "1440");

        // 2. 创建凭证
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 3. 配置载荷 (Claims)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, username)
        };

        // 4. 组装 Token
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(expireMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
