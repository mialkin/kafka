namespace Kafka.Confluent.Api.Endpoints.CreateUser.Models;

public record CreateUserRequest(string Name, int DepartmentId, bool IsActive);