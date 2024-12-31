using MediatR;
using Microsoft.Extensions.Logging;
using VsExample.Domain.Events;

namespace VsExample.Application.Features.Products.EventHandlers;

public class ProductDeleted
{
    public class Handler : INotificationHandler<ProductDeletedEvent>
    {
        private readonly ILogger<Handler> _logger;

        public Handler(ILogger<Handler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Product deleted: {@Notification}", notification);
        
            // TODO: Do more important things
        
            return Task.CompletedTask;
        }
    }    
}