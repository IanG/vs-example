using FluentAssertions;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using VsExample.Application.Features.Products.Commands;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Tests.Unit.Features.Products.Commands;

public class UpdateProductTests
{
    [Fact(DisplayName = "When Updating a valid Product it should be updated")]
    public async Task WhenUpdatingAValidProductItShouldBeAdded()
    {
        UpdateProduct.Validator validator = new();
        UpdateProduct.Handler handler = new(validator, GetApplicationDbContext());
        UpdateProduct.Command command = new(1, "Updated Name", 10m, "Updated Description");
        
        Result<bool> result = await handler.Handle(command, CancellationToken.None);
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [Fact(DisplayName = "When Updating an existing product with invalid data it should NOT be updated")]
    public async Task WhenUpdatingAnExistingProductWithInvalidDataItShouldNotBeUpdated()
    {
        UpdateProduct.Validator validator = new();
        UpdateProduct.Handler handler = new(validator, GetApplicationDbContext());
        UpdateProduct.Command command = new(1, "", -10m, "");
        
        Result<bool> result = await handler.Handle(command, CancellationToken.None);
        
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == "Update failed.");
    }

    [Fact(DisplayName = "When Updating a product that does not exist it should NOT be updated")]
    public async Task WhenUpdatingAProductThatDoesNotExistItShouldNotBeUpdated()
    {
        UpdateProduct.Validator validator = new();
        UpdateProduct.Handler handler = new(validator, GetApplicationDbContext());
        UpdateProduct.Command command = new(2, "New Name", 10m, "New Description");
        
        Result<bool> result = await handler.Handle(command, CancellationToken.None);
        
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == "Product not found.");
    }
    
    private ApplicationDbContext GetApplicationDbContext()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Products-" + Guid.NewGuid())
            .Options;

        ApplicationDbContext dbContext = new ApplicationDbContext(options);

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