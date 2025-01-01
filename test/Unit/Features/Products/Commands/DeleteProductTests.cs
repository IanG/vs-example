using FluentAssertions;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using VsExample.Application.Features.Products.Commands;
using VsExample.Domain.Events;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Tests.Unit.Features.Products.Commands;

public class DeleteProductTests
{
    [Fact(DisplayName = "When Deleting a Product that exists it should be deleted")]
    public async Task WhenDeletingAProductThatExistsItShouldBeDeleted()
    {
        
        ProductDeletedEvent expectedProductCreatedEvent = new(1, "ProductName", DateTime.Now);
        
        ProductDeletedEvent? publishedEvent = null;
        
        Mock<IMediator> mockMediator = new();
        
        mockMediator.Setup(m => m.Publish(It.IsAny<ProductDeletedEvent>(), It.IsAny<CancellationToken>()))
            .Callback<ProductDeletedEvent, CancellationToken>((@event, _) =>
            {
                publishedEvent = @event;
            });

        DeleteProduct.Handler handler = new(mockMediator.Object, GetApplicationDbContext());
        
        DeleteProduct.Command command = new(1);
        
        Result<bool> result = await handler.Handle(command, CancellationToken.None);
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
        
        publishedEvent.Should().NotBeNull();
        publishedEvent!.ProductId.Should().Be(expectedProductCreatedEvent.ProductId);
        publishedEvent.Name.Should().Be(expectedProductCreatedEvent.Name);
        publishedEvent.DeletedAt.Should().BeCloseTo(expectedProductCreatedEvent.DeletedAt, TimeSpan.FromSeconds(10));
        
        mockMediator.Verify(m => 
            m.Publish(It.IsAny<ProductDeletedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "When Deleting a Product that does NOT exist it should not be deleted")]
    public async Task WhenDeletingAProductThatDoesNotExistItShouldNotBeDeleted()
    {
        Mock<IMediator> mockMediator = new();
        
        DeleteProduct.Command command = new(2);
        
        DeleteProduct.Handler handler = new(mockMediator.Object, GetApplicationDbContext());
        
        Result<bool> result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == $"Product {command.Id} not found");
    }
    
    private ApplicationDbContext GetApplicationDbContext()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Products-" + Guid.NewGuid())
            .Options;

        ApplicationDbContext dbContext = new(options);

        dbContext.Products.Add(new()
        {
            Id = 1,
            Name = "ProductName",
            Description = "ProductDescription",
            Price = 10.99m,
            CreatedAt = DateTime.Now,
        });
        
        dbContext.SaveChanges();
        
        return dbContext;
    }
}