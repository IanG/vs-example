using FluentAssertions;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using VsExample.Application.Features.Products.Commands;
using VsExample.Application.Features.Products.Dtos;
using VsExample.Domain.Events;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Tests.Unit.Features.Products.Commands;

public class AddProductTests
{
    [Fact(DisplayName = "When Adding a valid Product it should be added")]
    public async Task WhenAddingAValidProductItShouldBeAdded()
    {
        ProductDto expectedProoductDto = new()
        {
            Id = 1,
            Name = "ProductName",
            Description = "ProductDescription",
            Price = 10.99m,
            CreatedAt = new DateTime()
        };

        ProductCreatedEvent expectedProductCreatedEvent = new(1, "ProductName", DateTime.Now);
        
        ProductCreatedEvent? publishedEvent = null;
        
        AddProduct.Validator validator = new();
        Mock<IMediator> mockMediator = new();
        
        mockMediator.Setup(m => m.Publish(It.IsAny<ProductCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Callback<ProductCreatedEvent, CancellationToken>((@event, _) =>
            {
                publishedEvent = @event;
            });
        
        AddProduct.Handler handler = new(validator, mockMediator.Object, GetEmptyApplicationDbContext());
        AddProduct.Command command = new("ProductName", 10.99m, "ProductDescription");
        
        Result<ProductDto> result = await handler.Handle(command, CancellationToken.None);
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(expectedProoductDto);
        
        publishedEvent.Should().NotBeNull();
        publishedEvent!.ProductId.Should().Be(expectedProductCreatedEvent.ProductId);
        publishedEvent.Name.Should().Be(expectedProductCreatedEvent.Name);
        publishedEvent.CreatedAt.Should().BeCloseTo(expectedProductCreatedEvent.CreatedAt, TimeSpan.FromSeconds(10));
        
        mockMediator.Verify(m => 
            m.Publish(It.IsAny<ProductCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "When Adding an invalid Product it should NOT be added")]
    public async Task WhenAddingAnInValidProductItShouldNotBeAdded()
    {
        AddProduct.Validator validator = new();

        Mock<IMediator> mockMediator = new();
        AddProduct.Handler handler = new(validator, mockMediator.Object, GetEmptyApplicationDbContext());
        AddProduct.Command command = new("", 0m, "");
        
        Result<ProductDto> result = await handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == "Validation failed.");
    }
    
    private ApplicationDbContext GetEmptyApplicationDbContext()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Products-" + Guid.NewGuid())
            .Options;

        ApplicationDbContext dbContext = new(options);
        
        return dbContext;
    }
}