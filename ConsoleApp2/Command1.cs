﻿namespace Pipeline
{
    public sealed class Command1 : ICommand
    {
        public async Task ExecuteAsync(Func<Task> next)
        {
            Console.WriteLine($"Executing {nameof(Command1)}");
            await next();
        }
    }
}