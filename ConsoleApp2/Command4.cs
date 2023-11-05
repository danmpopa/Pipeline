using Microsoft.Extensions.Logging;

namespace Pipeline
{
    public sealed class Command4 : ICommand<Command4Options>
    {
        public async Task ExecuteAsync(
            Func<Task> next,
            Action<Command4Options> action,
            IRequestResponseContext context,
            ILoggerFactory? loggerFactory)
        {
            Console.WriteLine($"Executing {nameof(Command4)} before next");

            var logger = loggerFactory?.CreateLogger<Command4>();

            Command4Options options = new()
            {
                IntProperty = 1,
                StringProperty = "Some text"
            };

            action.Invoke(options);

            Console.WriteLine(@$"Options are: 
                {nameof(Command4Options.IntProperty)}: {options.IntProperty}
                {nameof(Command4Options.StringProperty)}: {options.StringProperty}");

            var initResponse = context.Response;
            context.Response = $"{options.StringProperty} & {options.IntProperty}";

            await Console.Out.WriteLineAsync(@$"
                From value: '{initResponse}' to response '{context.Response}'");

            await next();

            logger?.LogDebug("Some debugging message with argument: '{SomeParam}'", "some argument");

            Console.WriteLine($"Finished {nameof(Command4)}");
        }
    }
}
