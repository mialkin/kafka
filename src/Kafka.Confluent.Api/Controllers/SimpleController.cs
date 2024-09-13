using Bogus;
using Kafka.Confluent.Api.Producers;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Confluent.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SimpleController : ControllerBase
{
    private readonly SimpleProducer _simpleProducer;
    private readonly Faker _faker;

    public SimpleController(SimpleProducer simpleProducer)
    {
        _simpleProducer = simpleProducer;
        _faker = new Faker("ru");
    }

    [HttpPost("produce-random-message")]
    public async Task<IActionResult> Get()
    {
        await _simpleProducer.ProduceAsync(_faker.Random.Word());

        return Ok();
    }
}