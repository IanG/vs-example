using MediatR;

namespace VsExample.Domain.Events;

public record ProductDeletedEvent(int ProductId, string Name, DateTime DeletedAt) : INotification;