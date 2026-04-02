using ApiTemplate.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ApiTemplate.Controllers;

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


    /// <summary>
    /// 返回异常信息
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ApiException"></exception>
    [HttpGet]
    [Route(nameof(GetExceptionResult1))]
    public object GetExceptionResult1()
    {
        // 需要定义 filePath 变量，假设为示例路径
        var filePath = "A.docx";
        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return File(stream, "application/pdf", "file.pdf");
    }


}
