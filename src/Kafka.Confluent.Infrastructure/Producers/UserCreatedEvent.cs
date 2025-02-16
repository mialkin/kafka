namespace Kafka.Confluent.Infrastructure.Producers;

public record UserCreatedEvent(Guid Id, string Name, int DepartmentId, bool IsActive);