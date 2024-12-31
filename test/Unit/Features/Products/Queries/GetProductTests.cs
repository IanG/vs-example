using FluentAssertions;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using VsExample.Application.Features.Products.Dtos;
using VsExample.Application.Features.Products.Queries;
using VsExample.Domain.Entities;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Tests.Unit.Features.Products.Queries;

public class GetProductTests
{
    private readonly GetProduct.Handler _handler;

    public GetProductTests()
    {
        _handler = new GetProduct.Handler(GetApplicationDbContext());
    }

    [Fact(DisplayName = "When a Product is requested by Id and it is found it is returned")]
    public async Task WhenAProductIsRequestedByIdAndItIsFoundItIsReturned()
    {
        int productId = 1;
        
        GetProduct.Query query = new GetProduct.Query(productId);

        Result<ProductDto?> result = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<ProductDto>();
        
        ProductDto? productDto = result.Value;
        
        productDto!.Id.Should().Be(productId);
        productDto.Name.Should().Be("Name");
        productDto.Price.Should().Be(33.4m);
        productDto.Description.Should().Be("Description");
    }

    [Fact(DisplayName = "When a Product is requested by Id and it is NOT found an error is returned")]
    public async Task WhenAProductIsRequestedByIdAndItIsNotFoundAnErrorIsReturned()
    {
        int productId = 2;
        
        GetProduct.Query query = new GetProduct.Query(productId);
        
        Result<ProductDto?> result = await _handler.Handle(query, CancellationToken.None);
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle()
            .Which.Message.Should().Be("Product not found");
    }

    private ApplicationDbContext GetApplicationDbContext()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Products-" + Guid.NewGuid())
            .Options;

        ApplicationDbContext dbContext = new ApplicationDbContext(options);

        dbContext.Products.Add(new Product()
        {
            Id = 1,
            Name = "Name",
            Price = 33.4m,
            Description = "Description",
        });
        
        dbContext.SaveChanges();
        
        return dbContext;
    }
}