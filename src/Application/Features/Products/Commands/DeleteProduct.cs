using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VsExample.Application.Abstractions.MediatR;
using VsExample.Domain.Entities;
using VsExample.Domain.Events;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Application.Features.Products.Commands;

public class DeleteProduct
{
    public sealed record Command (int Id) : ICommand<bool>;

    public class Handler : ICommandHandler<Command, bool>
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;

        public Handler(IMediator mediator, ApplicationDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(Command command, CancellationToken cancellationToken)
        {
            Product? product = await _dbContext.Products
             .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);
            
            if (product is null) return Result.Fail<bool>($"Product {command.Id} not found");

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            await _mediator.Publish(new ProductDeletedEvent(product.Id, product.Name, DateTime.Now), cancellationToken);
            
            return Result.Ok(true);
        }
    }
}