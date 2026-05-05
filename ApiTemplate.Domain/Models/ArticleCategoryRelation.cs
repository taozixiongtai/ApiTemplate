using SqlSugar;

namespace ApiTemplate.Domain.Models;

/// <summary>
/// 文章-分类关联实体模型
/// </summary>
[SugarTable("ArticleCategoryRelation")]
public class ArticleCategoryRelation
{
    /// <summary>
    /// 文章ID
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public int ArticleId { get; set; }

    /// <summary>
    /// 分类ID
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public int CategoryId { get; set; }
}