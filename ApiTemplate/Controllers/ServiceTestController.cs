using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ServiceTestController : ControllerBase
{
    private readonly ITestService _testService;
    public ServiceTestController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet("message")]
    public IActionResult GetMessage()
    {
        var message = _testService.GetMessage();
        return Ok(new { Message = message });
    }
}
