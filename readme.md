# Kafka

## Prerequisites

- [↑ .NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [↑ GNU Make](https://www.gnu.org/software/make)

## How to run application

```bash
make run-api
make run-grpc-server
```
or:

```bash
make  watch-api
make  watch-grpc-server
```

## How to run tests

```bash
make test
```

- Kafka API: <http://localhost:7010>
- Kafka UI: <http://localhost:7040>
