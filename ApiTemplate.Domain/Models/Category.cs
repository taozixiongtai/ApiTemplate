using SqlSugar;

namespace ApiTemplate.Domain.Models;

/// <summary>
/// 分类实体模型
/// </summary>
[SugarTable("Category")]
public class Category
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [SugarColumn(Length = 200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}