namespace ApiTemplate.Application.Dto;

/// <summary>
/// 分类数据传输对象
/// </summary>
public class CategoryDto
{
    /// <summary>
    /// 分类主键ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
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