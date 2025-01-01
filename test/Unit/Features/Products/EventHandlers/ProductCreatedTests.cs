using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using VsExample.Application.Features.Products.EventHandlers;
using VsExample.Domain.Events;

namespace VsExample.Tests.Unit.Features.Products.EventHandlers;

public class ProductCreatedTests
{
    [Fact(DisplayName = "Handler logs information when a product is created")]
    public async Task HandlerLogsInformationOnProductCreatedEvent()
    {
        var logger = new FakeLogger<ProductCreated.Handler>();
        var handler = new ProductCreated.Handler(logger);
        var notification = new ProductCreatedEvent(1, "Test Product", DateTime.UtcNow);
        
        await handler.Handle(notification, CancellationToken.None);

        IReadOnlyList<FakeLogRecord> logEntries = logger.Collector.GetSnapshot();
        
        logEntries.First().Level.Should().Be(LogLevel.Information);
        logEntries.First().Message.Should().StartWith("Product created");
    }
}