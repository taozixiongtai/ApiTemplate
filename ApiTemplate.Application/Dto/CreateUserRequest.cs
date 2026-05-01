namespace ApiTemplate.Application.Dto;

public record CreateUserRequest
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}