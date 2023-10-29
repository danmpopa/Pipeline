namespace Pipeline
{
    public sealed class Command2 : ICommand
    {
        public async Task ExecuteAsync(Func<Task> next)
        {
            await next();
            Console.WriteLine($"Executing {nameof(Command2)} after next");
        }
    }
}
