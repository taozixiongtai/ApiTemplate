using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.Application.Dto;

public record CategoryRequest
{
    [Required(ErrorMessage = "分类名称不能为空")]
    [MaxLength(200, ErrorMessage = "分类名称不能超过200个字符")]
    public string Name { get; set; } = string.Empty;
}