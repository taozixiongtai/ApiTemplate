using SqlSugar;

namespace ApiTemplate.Domain.Models;

/// <summary>
/// 用户实体模型
/// </summary>
[SugarTable]
public class User
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = false)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = false)]
    public string Password { get; set; } = string.Empty;  

    /// <summary>
    /// 昵称
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? Nickname { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? CreateTime { get; set; }
}
