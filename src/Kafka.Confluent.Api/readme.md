# Confluent Kafka usage in ASP.NET

This application is a showcase of [↑ Confluent's Apache Kafka .NET client](https://github.com/confluentinc/confluent-kafka-dotnet).

## Prerequisites

- [↑ .NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [↑ Docker Desktop](https://www.docker.com/products/docker-desktop/)

## Running application

Run infrastructure:

```bash
docker compose --file=infrastructure.yaml up --detach
```

Run application:

```bash
dotnet watch --no-hot-reload
```

Shutdown infrastructure:

```bash
docker compose --file=infrastructure.yaml down
```

## Links

[↑ Kafbat UI](http://localhost:7040/ui/clusters/local/all-topics).

[↑ Swagger UI](http://localhost:7010/).
