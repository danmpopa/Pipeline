namespace Pipeline
{
    public sealed class Command2 : ICommand<int>
    {
        public async Task ExecuteAsync(Func<Task> next, int @value)
        {
            await next();
            Console.WriteLine($"Executing {nameof(Command2)} after next --> {@value}");
        }
    }
}
