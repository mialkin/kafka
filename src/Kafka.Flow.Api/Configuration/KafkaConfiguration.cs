using Confluent.Kafka;
using Kafka.Flow.Api.Consumers;
using Kafka.Flow.Api.Producers;
using KafkaFlow;
using KafkaFlow.Serializer;

namespace Kafka.Flow.Api.Configuration;

public static class KafkaConfiguration
{
    public static void ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHelloMessageProducer, HelloMessageProducer>();

        var kafkaConfig = configuration.GetSection("Kafka").Get<KafkaConfig>();
        var consumerConfig = kafkaConfig!.GetConsumerConfig("example-consumer");

        services.AddKafka(
            kafka => kafka
                .UseConsoleLog()
                .AddCluster(
                    cluster => cluster
                        .WithBrokers(["localhost:7030"])
                        // .CreateTopicIfNotExists(topicName, numberOfPartitions: 1, replicationFactor: 1)
                        .AddProducer<HelloMessageProducer>(
                            builder => builder
                                .DefaultTopic(consumerConfig.TopicName)
                                .AddMiddlewares(
                                    middleware => middleware
                                        .AddSerializer<JsonCoreSerializer>())
                        )
                        .AddConsumer(consumer => consumer
                            .WithConsumerConfig(consumerConfig.ConsumerConfig)
                            .Topic(consumerConfig.TopicName)
                            .WithBufferSize(1)
                            .WithWorkersCount(1)
                            .AddMiddlewares(middlewares => middlewares
                                .AddDeserializer<JsonCoreDeserializer>()
                                .AddTypedHandlers(h => h.AddHandler<HelloMessageHandler>())
                            )
                        )
                )
        );
    }
}

public record KafkaConfig
{
    public required string[] Brokers { get; set; }
    public List<KafkaConsumerConfig> Consumers { get; set; } = [];
    public List<KafkaProducerConfig> Producers { get; set; } = [];

    public KafkaConsumerConfig GetConsumerConfig(string name)
    {
        return Consumers.FirstOrDefault(x => x.Name == name)
               ?? throw new InvalidOperationException($"KAFKA: Missing settings for consumer - {name}");
    }

    public KafkaProducerConfig GetProducerConfig(string name)
    {
        return Producers.FirstOrDefault(x => x.Name == name)
               ?? throw new InvalidOperationException($"KAFKA: Missing settings for producer - {name}");
    }
}

public record KafkaConsumerConfig
{
    public required string Name { get; set; }
    public required string TopicName { get; set; }
    public required ConsumerConfig ConsumerConfig { get; set; }
}

public record KafkaProducerConfig
{
    public required string ProducerName { get; set; }
    public required string Name { get; set; }
    public required string Topic { get; set; }
    public required ProducerConfig ProducerConfig { get; set; }
}