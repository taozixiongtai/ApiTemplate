namespace ApiTemplate.Application.Dto;

/// <summary>
/// 文章数据传输对象
/// </summary>
public class ArticleDto
{
    /// <summary>
    /// 文章主键ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 文章内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// 文章关联的分类列表
    /// </summary>
    public List<CategoryDto> Categories { get; set; } = new();
}