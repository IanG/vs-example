using FluentResults;
using MediatR;

namespace VsExample.Application.Abstractions.MediatR;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>;
    