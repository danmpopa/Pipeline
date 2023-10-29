﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public interface ICommandWithOptions<out TOptions>
    {
        TOptions Options { get; }
    }
}
