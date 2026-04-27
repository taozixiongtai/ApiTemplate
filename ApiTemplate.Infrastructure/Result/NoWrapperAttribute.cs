namespace ApiTemplate.Infrastructure.Result;

/// <summary>
/// 在控制器或 Action 上打上此特性，可跳过统一的结果包装
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class NoWrapperAttribute : Attribute
{
}