using System;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    [HttpGet("get-date")]
    public IActionResult Get()
    {
        return Ok(DateTime.UtcNow);
    }
}