using ApiTemplate.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiTemplate.Filter
{
    public class ApiResultWrapperFilter : IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var wrapped = ApiResult.Success(objectResult.Value);
                context.Result = new ObjectResult(wrapped)
                {
                    StatusCode = objectResult.StatusCode
                };
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
                context.Result = new JsonResult(ApiResult.Success(jsonResult.Value))
                {
                    StatusCode = jsonResult.StatusCode
                };
            }
            // 其他类型可根据需要扩展
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
