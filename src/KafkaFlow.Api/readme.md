# KafkaFlow

This is a bootstrap project that showcases how to create [↑ KafkaFlow](https://farfetch.github.io/kafkaflow/docs/) consumer and producer.

To produce a new ship order task visit this link: <http://localhost:7050/produce?orderNumber=12345>.

To produce a task via Kafka UI (http://localhost:7040) enter 

```json
{
  "OrderNumber":"12345"
}
```

inside `Value` input, and 

```json
{
  "Message-Type":"KafkaFlow.Infrastructure.Models.ShipOrderTask, KafkaFlow.Infrastructure"
}
```

inside `Headers`.
