// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
    .Run(() => WriteColloredLine("Run some delegate which can receive pipeline context", ConsoleColor.Magenta))
    .Use<Command1>()
    .Use<Command2, int>(23)
    .Use<Command3, string>("something to display")
    .Use<Command4, Command4Options>(options =>
    {
        options.StringProperty += "; added 'zzz'";
    })
    .Run(() => WriteColloredLine("And another delegate...", ConsoleColor.Cyan));

await pipeline.ExecuteAsync<PipelineContext>();

Console.ReadKey();

static void WriteColloredLine(string text, ConsoleColor consoleColor)
{
    var initialColor = Console.ForegroundColor;
    Console.ForegroundColor = consoleColor;
    Console.WriteLine(text);
    Console.ForegroundColor = initialColor;
}