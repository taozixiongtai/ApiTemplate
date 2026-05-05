using SqlSugar;

namespace ApiTemplate.Domain.Models;

/// <summary>
/// 文章实体模型
/// </summary>
[SugarTable("Article")]
public class Article
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    [SugarColumn(Length = 200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 文章内容
    /// </summary>
    [SugarColumn(ColumnDataType = "TEXT")]
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
    /// 关联的分类列表
    /// </summary>
    [Navigate(typeof(ArticleCategoryRelation), nameof(ArticleCategoryRelation.ArticleId), nameof(ArticleCategoryRelation.CategoryId))]
    public List<Category> Categories { get; set; } = new();
}