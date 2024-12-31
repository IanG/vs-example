using MediatR;
using Microsoft.Extensions.Logging;
using VsExample.Domain.Events;

namespace VsExample.Application.Features.Products.EventHandlers;

public class ProductCreated
{
    public class Handler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ILogger<Handler> _logger;

        public Handler(ILogger<Handler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Product created: {@Notification}", notification);
        
            // TODO: Do more important things
        
            return Task.CompletedTask;
        }
    }    
}
