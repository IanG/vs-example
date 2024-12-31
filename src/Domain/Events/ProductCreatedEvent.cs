using MediatR;

namespace VsExample.Domain.Events;

public record ProductCreatedEvent(int ProductId, string Name, DateTime CreatedAt) : INotification;
