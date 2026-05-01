using SqlSugar;

namespace ApiTemplate.Domain.Models;

[SugarTable]
public class User
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    [SugarColumn(Length = 50, IsNullable = false)]
    public string Username { get; set; } = string.Empty;

    [SugarColumn(Length = 100, IsNullable = false)]
    public string Password { get; set; } = string.Empty;  

    [SugarColumn(IsNullable = true)]
    public string? Nickname { get; set; }

    [SugarColumn(IsNullable = true)]
    public DateTime? CreateTime { get; set; }
}
