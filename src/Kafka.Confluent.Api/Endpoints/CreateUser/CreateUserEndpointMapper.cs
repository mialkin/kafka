using Kafka.Confluent.Api.Constants;
using Kafka.Confluent.Api.Endpoints.CreateUser.Models;
using Kafka.Confluent.Infrastructure.Producers;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Confluent.Api.Endpoints.CreateUser;

public static class CreateUserEndpointMapper
{
    public static void MapCreateUserEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost(
                pattern: EndpointNames.CreateUser,
                handler: async (
                    [FromBody] CreateUserRequest request,
                    [FromServices] SimpleProducer simpleProducer) =>
                {
                    await simpleProducer.ProduceAsync("ABC");

                    var userId = Guid.NewGuid();

                    return new CreateUserResponse(userId);
                })
            .Produces(StatusCodes.Status200OK, typeof(CreateUserResponse))
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new user");
    }
}