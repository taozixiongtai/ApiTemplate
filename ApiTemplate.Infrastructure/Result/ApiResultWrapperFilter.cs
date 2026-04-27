using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiTemplate.Infrastructure.Result;

public class ApiResultWrapperFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        // 1. 检查是否标记了跳过包装的特性
        var hasNoWrapper = context.ActionDescriptor.EndpointMetadata
            .Any(m => m is NoWrapperAttribute);

        if (hasNoWrapper)
        {
            await next();
            return;
        }

        // 2. 判断并包装结果，增加类型防重复包装检查
        if (context.Result is ObjectResult objectResult)
        {
            // 如果返回的本身已经是 ApiResult 类型，则不再重复包装
            if (objectResult.Value is not ApiResult)
            {
                objectResult.Value = ApiResult.Success(objectResult.Value);
            }
        }
        else if (context.Result is EmptyResult)
        {
            context.Result = new ObjectResult(ApiResult.Success())
            {
                StatusCode = 200
            };
        }
        else if (context.Result is JsonResult jsonResult)
        {
            if (jsonResult.Value is not ApiResult)
            {
                context.Result = new JsonResult(ApiResult.Success(jsonResult.Value))
                {
                    StatusCode = jsonResult.StatusCode
                };
            }
        }

        // 其他类型 (例如 FileResult, ContentResult 等) 原样放行

        await next();
    }
}