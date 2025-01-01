using FluentResults;
using MediatR;

namespace VsExample.Application.Abstractions.MediatR;

public interface ICommand<TResult> : IRequest<Result<TResult>>, ICommand;
public interface ICommand;
