﻿using Microsoft.Extensions.Logging;

namespace Pipeline
{
#pragma warning disable S2326 // Unused type parameters should be removed
    public interface ICommand<out TOptions>
#pragma warning restore S2326 // Unused type parameters should be removed
    {
        // marker interface
    }
}