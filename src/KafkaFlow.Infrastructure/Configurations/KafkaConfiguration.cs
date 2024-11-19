using KafkaFlow.Infrastructure.Consumers;
using KafkaFlow.Infrastructure.Models;
using KafkaFlow.Infrastructure.Producers;
using KafkaFlow.Infrastructure.Settings;
using KafkaFlow.Serializer;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaFlow.Infrastructure.Configurations;

public static class KafkaConfiguration
{
    public static void ConfigureKafka(this IServiceCollection services, KafkaSettings kafkaSettings)
    {
        var consumerSettings = kafkaSettings.GetConsumerSettings(KafkaConstants.ConsumerNames.ShipOrderTasks);

        services.AddKafka(
            kafka => kafka
                .UseConsoleLog()
                .AddCluster(
                    cluster => cluster
                        .WithBrokers(kafkaSettings.Brokers)
                        .AddConsumer(
                            consumer => consumer
                                .WithConsumerConfig(consumerSettings.ConsumerConfig)
                                .Topic(consumerSettings.Topic)
                                .WithWorkersCount(consumerSettings.WorkersCount)
                                .WithBufferSize(consumerSettings.BufferSize)
                                .AddMiddlewares(
                                    middlewares => middlewares
                                        .AddDeserializer<JsonCoreDeserializer>()
                                        .AddTypedHandlers(builder => builder.AddHandler<ShipOrderTaskHandler>())
                                )
                        )
                        .AddProducer<ShipOrderTask>(
                            producer =>
                            {
                                producer
                                    .DefaultTopic("ship-order-tasks")
                                    .WithAcks(Acks.All)
                                    .AddMiddlewares(middlewares => middlewares.AddSerializer<JsonCoreSerializer>());
                            })
                )
        );

        services.AddSingleton<IShipOrderTaskProducer, ShipOrderTaskProducer>();
    }
}