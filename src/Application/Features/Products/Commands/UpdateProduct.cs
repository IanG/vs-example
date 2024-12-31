using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using VsExample.Application.Abstractions.MediatR;
using VsExample.Domain.Entities;
using VsExample.Infrastructure.Persistence;

namespace VsExample.Application.Features.Products.Commands;

public class UpdateProduct
{
    public sealed record Command(int Id, string Name, decimal Price, string? Description) : ICommand<bool>;

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
                .Length(10, 500).WithMessage("Product description must be between 10 and 500 characters.");
        }
    }

    public class Handler : ICommandHandler<Command, bool>
    {
        private readonly IValidator<Command> _validator;
        private readonly ApplicationDbContext _dbContext;
        
        public Handler(IValidator<Command> validator, ApplicationDbContext dbContext)
        {
            _validator = validator;
            _dbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(Command command, CancellationToken cancellationToken)
        {
            ValidationResult? validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid) return false;
            
            Product? product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

            if (product is null) return false;
            
            product.Name = command.Name;
            product.Description = command.Description!;
            product.Price = command.Price;
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
    }
}