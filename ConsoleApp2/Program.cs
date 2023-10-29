// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Pipeline;

IServiceProvider serviceProvider = new ServiceCollection()
    .AddLogging((loggingBuilder) => loggingBuilder
        .SetMinimumLevel(LogLevel.Debug)
        .AddConsole()
        )
    .BuildServiceProvider();

var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
var logger = loggerFactory?.CreateLogger<Program>();

logger?.LogInformation("Start creating the pipeline:");

Pipeline.Pipeline pipeline = 
    new Pipeline.Pipeline(loggerFactory)
    .Use<Command1>()
    .Use<Command2>()
    .Use<Command3>()
    .Use<Command4, Command4Options>(options =>
    {
        options.StringProperty += "; added 'zzz'";
    });

await pipeline.ExecuteAsync<PipelineContext>();

Console.Read();
