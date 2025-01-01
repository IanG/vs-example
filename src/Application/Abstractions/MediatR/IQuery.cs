using FluentResults;
using MediatR;

namespace VsExample.Application.Abstractions.MediatR;

public interface IQuery<TResult> : IRequest<Result<TResult>>, IQuery;
public interface IQuery;