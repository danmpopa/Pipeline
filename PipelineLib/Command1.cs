namespace Pipeline
{
    public sealed class Command1
    {
        public async Task ExecuteAsync(Func<Task> next)
        {
            Console.WriteLine($"Executing {nameof(Command1)}");
            await next();
            Console.WriteLine($"Finished {nameof(Command1)}");
        }
    }
}
