using ApiTemplate.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiResultController : ControllerBase
    {
        /// <summary>
        /// 正常返回结果
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object GetNormalResult()
        {
            // 返回匿名对象
            return new { Name = "nnn", Age = 12 };
        }

        /// <summary>
        /// 返回异常信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpGet]
        [Route(nameof(GetExceptionResult))]
        public object GetExceptionResult()
        {
            throw new ApiException("错误信息");
            return string.Empty;
        }
    }
}
