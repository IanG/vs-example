using FluentResults;
using MediatR;

namespace VsExample.Application.Abstractions.MediatR;

// Before introducing FluentResults
// public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
//     where TCommand : ICommand<TResult>;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>;