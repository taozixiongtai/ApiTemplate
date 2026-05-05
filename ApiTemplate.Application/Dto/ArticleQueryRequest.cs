using ApiTemplate.Domain.Dto;

namespace ApiTemplate.Application.Dto;

/// <summary>
/// 文章查询请求对象
/// </summary>
public class ArticleQueryRequest : PageRequest
{
    /// <summary>
    /// 分类ID
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// 搜索关键字（匹配标题或内容）
    /// </summary>
    public string? KeyWord { get; set; }
}