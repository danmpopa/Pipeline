using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public sealed class PipelineContext : IRequestResponseContext
    {
        public object? Request { get; set; }
        public object? Response { get; set; }
    }
}
