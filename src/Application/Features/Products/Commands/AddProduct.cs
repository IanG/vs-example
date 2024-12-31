using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using VsExample.Application.Abstractions.MediatR;
using VsExample.Application.Features.Products.Dtos;
using VsExample.Domain.Entities;
using VsExample.Domain.Events;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Application.Features.Products.Commands;

public class AddProduct
{
    public sealed record Command (string Name, decimal Price, string Description) : ICommand<ProductDto>;
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Description)
                .Length(1, 500).WithMessage("Product description must be between 1 and 500 characters.");
        }
    }
    
    public class Handler : ICommandHandler<Command, ProductDto>
    {
        private readonly IValidator<Command> _validator;
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;

        public Handler(IValidator<Command> validator, IMediator mediator, ApplicationDbContext dbContext)
        {
            _validator = validator;
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task<Result<ProductDto>> Handle(Command command, CancellationToken cancellationToken)
        {
            ValidationResult? validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid) return Result.Fail("Validation failed.");
            
            Product product = new()
            {
                Name = command.Name,
                Price = command.Price,
                Description = command.Description,
                CreatedAt = DateTime.Now
            };
            
            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            await _mediator.Publish(new ProductCreatedEvent(product.Id, product.Name, product.CreatedAt), cancellationToken);

            ProductDto productDto = new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
            
            return Result.Ok(productDto);
        }
    }
}