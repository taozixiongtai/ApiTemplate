using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Application.Dto;

/// <summary>
/// 文章创建/更新请求参数
/// </summary>
public record ArticleRequest
{
    /// <summary>
    /// 文章标题
    /// </summary>
    [Required(ErrorMessage = "文章标题不能为空")]
    [MaxLength(200, ErrorMessage = "文章标题不能超过200个字符")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 文章内容
    /// </summary>
    [Required(ErrorMessage = "文章内容不能为空")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 分类ID集合
    /// </summary>
    public List<int> CategoryIds { get; set; } = [];
}