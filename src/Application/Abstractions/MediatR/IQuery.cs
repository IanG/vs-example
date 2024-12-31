using FluentResults;
using MediatR;

namespace VsExample.Application.Abstractions.MediatR;

// Before introducing FluentResults
// public interface IQuery<out TResult> : IRequest<TResult>;

public interface IQuery<TResult> : IRequest<Result<TResult>>, IQuery;
public interface IQuery;