using System.Threading.Tasks;
using Kafka.Api.Producers;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SimpleController : ControllerBase
{
    private readonly SimpleProducer _simpleProducer;

    public SimpleController(SimpleProducer simpleProducer)
    {
        _simpleProducer = simpleProducer;
    }

    [HttpPost("produce-message")]
    public async Task<IActionResult> Get(string message)
    {
        await _simpleProducer.ProduceAsync(message);

        return Ok();
    }
}