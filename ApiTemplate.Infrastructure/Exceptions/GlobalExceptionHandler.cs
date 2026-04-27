using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Infrastructure.Result;

namespace ApiTemplate.Infrastructure.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        var message = exception is ApiException apiEx ? apiEx.Message : "服务器内部错误";
        var response = ApiResult.Fail(message);

        await context.Response.WriteAsJsonAsync(response, cancellationToken);

        // 返回 true 表示异常已被处理，中间件管道停止传递此异常
        return true;
    }
}