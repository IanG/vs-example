using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;

namespace VsExample.Infrastructure.Logging;

[ExcludeFromCodeCoverage]
public static class HostBuilderExtensions
{
    public static void AddLogging(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        Logger logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        hostBuilder.UseSerilog(logger);
    }
}