// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Logging;
using Pipeline;

Console.WriteLine("Hello World");

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("System", LogLevel.Warning)
        .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug);
        //.AddConsole()
        //.AddEventLog();
});
ILogger logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Start creating the pipeline:");

Pipeline.Pipeline pipeline = 
    new Pipeline.Pipeline()
    .Use<Command1>()
    .Use<Command2>()
    .Use<Command3>()
    .Use<Command4, Command4Options>(options =>
    {
        options.StringProperty += "; added 'zzz'";
    });

await pipeline.ExecuteAsync<PipelineContext>();

Console.Read();
