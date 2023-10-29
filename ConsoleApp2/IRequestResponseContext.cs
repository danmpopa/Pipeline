namespace Pipeline
{
    public interface IRequestResponseContext : IRequestContext
    {
        object? Response { get; set; }
    }
}