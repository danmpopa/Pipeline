using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public sealed class Command3 : ICommand
    {
        public async Task ExecuteAsync(Func<Task> next)
        {
            Console.WriteLine($"Executing {nameof(Command3)} before next");
            await next();
            Console.WriteLine($"Executing {nameof(Command3)} after next");
        }
    }
}
