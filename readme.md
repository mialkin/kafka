# Kafka

## Prerequisites

- [↑ .NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [↑ GNU Make](https://www.gnu.org/software/make)

## How to run application

1\. First run infrastructure:

```bash
make run-infrastructure
```

To shut down infrastructure, when not needed anymore, run:

```bash
make shutdown-infrastructure
```

2\. Run application:

```bash
make run-confluent
make run-flow
```
or:

```bash
make watch-confluent
make watch-flow
```
