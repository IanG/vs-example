using FluentAssertions;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using VsExample.Application.Features.Products.Dtos;
using VsExample.Application.Features.Products.Queries;
using VsExample.Domain.Entities;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Tests.Unit.Features.Products.Queries;

public class GetProductsTests
{
    [Fact(DisplayName = "When No Products exist an empty list will be returned")]
    public async Task WhenNoProductsExistAnEmptyListWillBeReturned()
    {
        GetProducts.Handler handler = new GetProducts.Handler(GetEmptyApplicationDbContext());
        GetProducts.Query query = new GetProducts.Query(PageNumber: 1, PageSize: 10);
        
        Result<ProductQueryResult> result = await handler.Handle(query, CancellationToken.None);
        
        result.IsSuccess.Should().BeTrue();
        
        ProductQueryResult productQueryResult = result.Value;
        
        productQueryResult.Products.Should().BeEmpty();
        productQueryResult.TotalCount.Should().Be(0);
    }

    [Fact(DisplayName = "When Products exist they should be returned")]
    public async Task WhenProductsExistTheyShouldBeReturned()
    {
        List<ProductDto> expectedProductDtos = [
            new() { Id = 1, Name = "Name", Price = 33.4m, Description = "Description" }
        ];
        
        GetProducts.Handler handler = new GetProducts.Handler(GetPopulatedApplicationDbContext());
        GetProducts.Query query = new GetProducts.Query(PageNumber: 1, PageSize: 10);
        
        Result<ProductQueryResult> result = await handler.Handle(query, CancellationToken.None);
        
        result.IsSuccess.Should().BeTrue();
        
        ProductQueryResult productQueryResult = result.Value;
        
        productQueryResult.Products.Should().NotBeEmpty();
        productQueryResult.Products.Should().BeEquivalentTo(expectedProductDtos);
    }
    
    private ApplicationDbContext GetEmptyApplicationDbContext()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Products-" + Guid.NewGuid())
            .Options;

        ApplicationDbContext dbContext = new ApplicationDbContext(options);
        
        return dbContext;
    }
    
    private ApplicationDbContext GetPopulatedApplicationDbContext()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Products-" + Guid.NewGuid())
            .Options;

        ApplicationDbContext dbContext = new ApplicationDbContext(options);
        
        dbContext.Products.AddRange(
            new Product { Id = 1, Name = "Name", Price = 33.4m, Description = "Description" }
        );

        dbContext.SaveChanges();
        
        return dbContext;
    }
}