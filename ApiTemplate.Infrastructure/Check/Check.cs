using ApiTemplate.Infrastructure.Exceptions;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace ApiTemplate.Infrastructure.Check;

/// <summary>
/// 业务逻辑检查工具类。如果不符合条件，将抛出统一的业务异常 (ApiException)
/// </summary>
public static class Check
{
    /// <summary>
    /// 检查对象是否为空，为空则抛出 ApiException
    /// </summary>
    public static void NotNull([NotNull] object? value, string message)
    {
        if (value is null)
        {
            throw new ApiException(message);
        }
    }

    /// <summary>
    /// 检查字符串是否为空或纯空格，为空则抛出 ApiException
    /// </summary>
    public static void NotEmpty([NotNull] string? value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ApiException(message);
        }
    }

    /// <summary>
    /// 检查条件是否为 true，为 false 则抛出 ApiException
    /// </summary>
    public static void IsTrue(bool condition, string message)
    {
        if (!condition)
        {
            throw new ApiException(message);
        }
    }

    /// <summary>
    /// 检查条件是否为 false，为 true 则抛出 ApiException
    /// </summary>
    public static void IsFalse(bool condition, string message)
    {
        if (condition)
        {
            throw new ApiException(message);
        }
    }

    /// <summary>
    /// 检查对象是否为 null，如果不为 null 则抛出 ApiException
    /// </summary>
    public static void IsNull(object? value, string message)
    {
        if (value is not null)
        {
            throw new ApiException(message);
        }
    }

    /// <summary>
    /// 检查字符串是否不为空且不为纯空格，如果为空则抛出 ApiException（与 NotEmpty 语义相反，这里是指要求它必须为空才能通过）
    /// </summary>
    public static void IsEmpty(string? value, string message)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            throw new ApiException(message);
        }
    }

    /// <summary>
    /// 检查集合是否为空或不存在，如果不为空则抛出 ApiException
    /// </summary>
    public static void IsEmpty(IEnumerable? collection, string message)
    {
        if (collection is not null)
        {
            var enumerator = collection.GetEnumerator();
            if (enumerator.MoveNext())
            {
                throw new ApiException(message);
            }
        }
    }

    /// <summary>
    /// 检查集合是否不为空且包含元素，如果为空或不存在则抛出 ApiException
    /// </summary>
    public static void NotEmpty([NotNull] IEnumerable? collection, string message)
    {
        if (collection is null)
        {
            throw new ApiException(message);
        }

        var enumerator = collection.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            throw new ApiException(message);
        }
    }
  
}