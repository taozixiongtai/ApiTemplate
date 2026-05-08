using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Application.Dto;

/// <summary>
/// 分类创建/更新请求参数
/// </summary>
public record CategoryRequest
{
    /// <summary>
    /// 分类名称
    /// </summary>
    [Required(ErrorMessage = "分类名称不能为空")]
    [MaxLength(200, ErrorMessage = "分类名称不能超过200个字符")]
    public string Name { get; set; } = string.Empty;
}