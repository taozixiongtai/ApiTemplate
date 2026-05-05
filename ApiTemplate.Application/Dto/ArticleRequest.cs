namespace ApiTemplate.Application.Dto;

public class ArticleRequest
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public List<int> CategoryIds { get; set; } = [];
}