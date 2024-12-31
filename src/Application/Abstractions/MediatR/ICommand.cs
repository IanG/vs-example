using FluentResults;
using MediatR;

namespace VsExample.Application.Abstractions.MediatR;

// Before introducing FluentResults
// public interface ICommand<out TResult> : IRequest<TResult>;

public interface ICommand<TResult> : IRequest<Result<TResult>>, ICommand;
public interface ICommand;
