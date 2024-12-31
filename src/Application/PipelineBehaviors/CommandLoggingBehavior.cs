using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VsExample.Application.Abstractions.MediatR;

namespace VsExample.Application.PipelineBehaviors;

[ExcludeFromCodeCoverage]
public class CommandLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<CommandLoggingBehaviour<TRequest, TResponse>> _logger;

    public CommandLoggingBehaviour(ILogger<CommandLoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is ICommand)
        {
            Type commandType = typeof(TRequest);
            string declaringType = commandType.DeclaringType != null 
                ? $"{commandType.DeclaringType.Name}.{commandType.Name}" 
                : commandType.Name;

            try
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                    _logger.LogDebug("Handling command: {RequestType} with: {@Request}", declaringType, request);

                Stopwatch stopwatch = Stopwatch.StartNew();
                TResponse response = await next();
                stopwatch.Stop();

                if (_logger.IsEnabled(LogLevel.Debug))
                    _logger.LogDebug("Handled command: {RequestType} in {ElapsedMilliseconds} ms", declaringType, stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Command: {RequestType} failed with error: {ErrorMessage}", declaringType, ex.Message);
                throw;
            }
        }

        return await next();
    }
}