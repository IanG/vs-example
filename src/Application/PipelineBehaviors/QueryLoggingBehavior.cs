using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using FluentResults;
using VsExample.Application.Abstractions.MediatR;

namespace VsExample.Application.PipelineBehaviors;

[ExcludeFromCodeCoverage]
public class QueryLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<QueryLoggingBehavior<TRequest, TResponse>> _logger;

    public QueryLoggingBehavior(ILogger<QueryLoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is IQuery)
        {
            // Get the full name including the outer class, if present
            Type queryType = typeof(TRequest);
            string declaringType = queryType.DeclaringType != null 
                ? $"{queryType.DeclaringType.Name}.{queryType.Name}" 
                : queryType.Name;

            try
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                    _logger.LogDebug("Handling query: {RequestType} with: {@Request}", declaringType, request);

                Stopwatch stopwatch = Stopwatch.StartNew();
                TResponse response = await next();
                stopwatch.Stop();

                if (_logger.IsEnabled(LogLevel.Debug))
                    _logger.LogDebug("Handled query: {RequestType} in {ElapsedMilliseconds} ms", declaringType, stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Query: {RequestType} failed with error: {ErrorMessage}", declaringType, ex.Message);
                throw;
            }
        }

        return await next();
    }
}