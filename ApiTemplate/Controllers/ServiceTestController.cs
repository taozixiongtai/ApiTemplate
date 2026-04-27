using ApiTemplate.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceTestController(ITestService testService) : ControllerBase
{

    [HttpGet("message")]
    public IActionResult GetMessage()
    {
        var message = testService.GetMessage();
        return Ok(new { Message = message });
    }
}
