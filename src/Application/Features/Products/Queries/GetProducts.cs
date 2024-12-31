using FluentResults;
using Microsoft.EntityFrameworkCore;
using VsExample.Application.Abstractions.MediatR;
using VsExample.Application.Features.Products.Dtos;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Application.Features.Products.Queries;

public class GetProducts
{
    public sealed record Query(string? Filter = null, int PageNumber = 1, int PageSize = 10) : IQuery<ProductQueryResult>;
    
    public class Handler : IQueryHandler<Query, ProductQueryResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<ProductQueryResult>> Handle(Query query, CancellationToken cancellationToken)
        {
            int skip = (query.PageNumber - 1) * query.PageSize;
            int totalCount = await _dbContext.Products.CountAsync(cancellationToken);
            
            IEnumerable<ProductDto> productsDtos = await _dbContext.Products
                .Skip(skip)
                .Take(query.PageSize)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync(cancellationToken);
            
            return Result.Ok(new ProductQueryResult
            {
                TotalCount = totalCount,
                Products = productsDtos
            });
        }
    }
}