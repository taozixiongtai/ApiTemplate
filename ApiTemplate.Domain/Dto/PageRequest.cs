namespace ApiTemplate.Domain.Dto;

/// <summary>
/// 分页请求基类
/// </summary>
public record PageRequest
{
    private int _page = 1;
    private int _pageSize = 10;

    /// <summary>
    /// 页码 (默认为 1，最小为 1)
    /// </summary>
    public int Page 
    { 
        get => _page; 
        set => _page = value < 1 ? 1 : value; 
    }

    /// <summary>
    /// 每页条数 (默认为 10，最小为 1，最大为 100)
    /// </summary>
    public int PageSize 
    { 
        get => _pageSize; 
        set => _pageSize = value < 1 ? 1 : (value > 100 ? 100 : value); 
    }
}