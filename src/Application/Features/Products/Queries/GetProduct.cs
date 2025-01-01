using FluentResults;
using Microsoft.EntityFrameworkCore;
using VsExample.Application.Abstractions.MediatR;
using VsExample.Application.Features.Products.Dtos;
using VsExample.Domain.Entities;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Application.Features.Products.Queries;

public class GetProduct
{
    public sealed record Query (int Id) : IQuery<ProductDto?>;
    
    public class Handler : IQueryHandler<Query, ProductDto?>
    {
        private readonly ApplicationDbContext _dbContext;

        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<ProductDto?>> Handle(Query query, CancellationToken cancellationToken)
        {
            Product? product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

            if (product is not null)
            {
                ProductDto productDto = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    CreatedAt = product.CreatedAt,
                };
                
                return Result.Ok(productDto)!;
            }
            
            return Result.Fail("Product not found");
        }
    }
}