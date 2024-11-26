using KafkaFlow.Infrastructure.Constants;
using KafkaFlow.Infrastructure.Consumers;
using KafkaFlow.Infrastructure.Models;
using KafkaFlow.Infrastructure.Producers;
using KafkaFlow.Infrastructure.Settings;
using KafkaFlow.Middlewares.Serializer.Resolvers;
using KafkaFlow.Serializer;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaFlow.Infrastructure.Configurations;

public static class KafkaConfiguration
{
    public static void ConfigureKafka(this IServiceCollection services, KafkaSettings kafkaSettings)
    {
        var consumerSettings = kafkaSettings.GetConsumerSettings(KafkaConsumerNames.ShipOrderTasks);
        var producerSettings = kafkaSettings.GetProducerSettings(KafkaProducerNames.ShipOrderResult);

        services.AddKafka(
            kafka => kafka
                .UseConsoleLog()
                .AddCluster(
                    cluster => cluster
                        .WithBrokers(kafkaSettings.Brokers)
                        .AddConsumer(
                            consumer => consumer
                                .WithConsumerConfig(consumerSettings.Configuration)
                                .Topic(consumerSettings.Topic)
                                .WithWorkersCount(consumerSettings.WorkersCount)
                                .WithBufferSize(consumerSettings.BufferSize)
                                .AddMiddlewares(
                                    middlewares => middlewares
                                        // .AddDeserializer<JsonCoreDeserializer>()
                                        .AddSingleTypeDeserializer<JsonCoreDeserializer>(typeof(MessageTypeB))
                                )
                        )
                        .AddProducer<MessageTypeA>(
                            producer =>
                            {
                                producer
                                    .WithProducerConfig(producerSettings.Configuration)
                                    .DefaultTopic(producerSettings.Topic)
                                    // .WithAcks(Acks.All)
                                    .AddMiddlewares(
                                        middlewares =>
                                            middlewares.AddSingleTypeSerializer<JsonCoreSerializer>(
                                                typeof(MessageTypeA)));
                            })
                )
        );

        services.AddSingleton<IShipOrderTaskProducer, ShipOrderTaskProducer>();
    }
}

// class MyMessageTypeResolver : IMessageTypeResolver
// {
//     public ValueTask<Type> OnConsumeAsync(IMessageContext context) => new(typeof(MessageTypeB));
//     public ValueTask OnProduceAsync(IMessageContext context) => default;
// }