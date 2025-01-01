using FluentResults;
using MediatR;

namespace VsExample.Application.Abstractions.MediatR;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>;