namespace Pipeline
{
    public sealed class Command3 : ICommand<string>
    {
        public async Task ExecuteAsync(Func<Task> next, string textToDisplay)
        {
            Console.WriteLine($"Executing {nameof(Command3)} before next --> '{(string.IsNullOrWhiteSpace(textToDisplay) ? "null" : textToDisplay)}'");
            await next(); 
            Console.WriteLine($"Finished {nameof(Command3)}");
        }
    }
}
