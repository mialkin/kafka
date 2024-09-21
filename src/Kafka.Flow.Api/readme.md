# KafkaFlow

This is a bootstrap project that showcases how to create [↑ KafkaFlow](https://farfetch.github.io/kafkaflow/docs/) consumer and producer.

To produce a `Hello world` message visit this link: <http://localhost:7050/produce?message=Hello%20world.>

To produce message via Kafka UI (http://localhost:7040) enter 

```json
{
  "Text":"Hello world"
}
```

`Hello world` inside `Value` input,
and 

```json
{
  "Message-Type":"Kafka.Flow.Api.Models.HelloMessage, Kafka.Flow.Api"
}
```

inside `Headers`.
