using FluentResults;
using MediatR;

namespace VsExample.Application.Abstractions.MediatR;

// Before introducing FluentResults
// public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
//     where TQuery : IQuery<TResult>;
    
public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>;
    