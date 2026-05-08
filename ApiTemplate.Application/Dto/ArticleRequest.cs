using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Application.Dto;

public record ArticleRequest
{
    [Required(ErrorMessage = "文章标题不能为空")]
    [MaxLength(200, ErrorMessage = "文章标题不能超过200个字符")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "文章内容不能为空")]
    public string Content { get; set; } = string.Empty;

    public List<int> CategoryIds { get; set; } = [];
}