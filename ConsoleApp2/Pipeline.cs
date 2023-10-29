using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Reflection;

namespace Pipeline
{
    public sealed class Pipeline
    {
        private readonly ConcurrentQueue<KeyValuePair<Type, object?>> _actions = new();
        private readonly ILogger<Pipeline>? _logger;
        private readonly ILoggerFactory? _loggerFactory;
        private object? _context;

        public Pipeline(ILoggerFactory? loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory?.CreateLogger<Pipeline>();

            _logger?.LogDebug("{Module} initialized", $"{nameof(Pipeline)}");
        }

        public Pipeline Use<TComand>() where TComand : ICommand
        {
            _actions.Enqueue(new KeyValuePair<Type, object?>(typeof(TComand), null));
            return this;
        }

        public Pipeline Use<TCommand, TOptions>(Action<TOptions>? action = null) where TCommand : ICommand<TOptions>
        {
            _actions.Enqueue(new KeyValuePair<Type, object?>(typeof(TCommand), action));
            return this;
        }

        public async Task ExecuteAsync<TContext>(Action<TContext>? options = null) 
            where TContext : IRequestContext, new()
        {
            /*
             * Is a pipeline without a context a real scenario? 
             * If true then maybe instantiate the context when options is non-null?
            */
            _context = new TContext();

            options?.Invoke((TContext)_context);

            if(_actions.TryDequeue(out var action))
            {
                await InvokeMethodAsync(action.Key, action.Value, (TContext)_context);
            }

            Console.WriteLine("Pipeline finished the actions executions!");
        }

        private async Task ExecuteNext()
        {
            if (_actions.TryDequeue(out var action))
            {
                await InvokeMethodAsync(action.Key, action.Value, _context);
                return;
            }

            Console.WriteLine("Last chained action...");
        }

        private async Task InvokeMethodAsync(Type type, object? actionArg, object? context)
        {
            const string methodName = "ExecuteAsync";

            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(mi => mi.Name.Equals(methodName, comparisonType: StringComparison.OrdinalIgnoreCase));
            var parameters = methods.First().GetParameters();

            var arguments = new object[parameters.Length];
            for (int counter = 0; counter < parameters.Length; counter++) 
            {
                var parameter = parameters[counter];
                var parameterType = parameters[counter].ParameterType;

                if (parameterType.IsGenericType
                    && parameterType.GetGenericTypeDefinition() == typeof(Func<>)
                    && parameter!.Name.Equals("next", StringComparison.OrdinalIgnoreCase))
                {
                    arguments[counter] = ExecuteNext;
                    continue;
                }

                if (actionArg?.GetType().Equals(parameterType) == true)
                {
                    arguments[counter] = actionArg;
                    continue;
                }

                if (parameterType.IsInstanceOfType(context))
                {
                    arguments[counter] = context;
                    continue;
                }

                if (parameterType.IsAssignableFrom(typeof(ILoggerFactory)))
                {
                    arguments[counter] = _loggerFactory;
                    continue;
                }
            }

            var method = methods.First() ?? throw new MissingMethodException(type.Name, methodName);
            var instance = Activator.CreateInstance(type);
            await (Task)method?.Invoke(instance, arguments!);
        }
    }
}
