namespace ApiTemplate.Domain.Dto;

/// <summary>
/// 分页结果
/// </summary>
/// <typeparam name="T">数据项类型</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// 数据列表
    /// </summary>
    public List<T> Items { get; set; } = new();

    /// <summary>
    /// 总记录数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 当前页码
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}