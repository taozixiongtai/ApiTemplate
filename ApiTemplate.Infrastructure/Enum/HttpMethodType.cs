namespace ApiTemplate.Infrastructure.Enum;

/// <summary>
/// HTTP 请求方法类型
/// </summary>
public enum HttpMethodType
{
    /// <summary>
    /// POST 请求 (通常用于新增数据)
    /// </summary>
    POST = 0,

    /// <summary>
    /// GET 请求 (通常用于查询数据)
    /// </summary>
    GET = 1,

    /// <summary>
    /// PUT 请求 (通常用于整体更新数据)
    /// </summary>
    PUT = 2,

    /// <summary>
    /// DELETE 请求 (通常用于删除数据)
    /// </summary>
    DELETE = 3,

    /// <summary>
    /// PATCH 请求 (通常用于局部更新数据)
    /// </summary>
    PATCH = 4
}