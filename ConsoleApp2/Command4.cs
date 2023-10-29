﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public sealed class Command4 : ICommand<Command4Options>
    {
        public async Task ExecuteAsync(
            Func<Task> next,
            Action<Command4Options> action,
            IRequestContext inputOutputContext,
            ILogger<Command4>? logger)
        {
            Console.WriteLine($"Executing {nameof(Command4)} before next");

            Command4Options options = new()
            {
                IntProperty = 1,
                StringProperty = "Some text"
            };

            action.Invoke(options);

            Console.WriteLine(@$"Options are: 
                    {nameof(Command4Options.IntProperty)}: {options.IntProperty}
                    {nameof(Command4Options.StringProperty)}: {options.StringProperty}");

            await next();

            logger?.LogDebug("Some debugging message with argument: '{SomeParam}'", "some argument");

            Console.WriteLine($"Executing {nameof(Command4)} after next");
        }
    }
}
