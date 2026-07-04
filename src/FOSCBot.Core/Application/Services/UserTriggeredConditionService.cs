using System.Collections.Concurrent;
using FOSCBot.Core.Module.Options;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Application.Services;

/// <summary>
/// Compiles and runs the C# condition snippets configured for user-triggered responses.
/// Conditions come from appsettings and run with full trust: whoever edits the
/// configuration can execute arbitrary code, the same trust level as deploying the bot.
/// </summary>
public class UserTriggeredConditionService(ILogger<UserTriggeredConditionService> logger)
{
    private static readonly ScriptOptions Options = ScriptOptions.Default
        .AddReferences(typeof(Message).Assembly)
        .AddImports("System", "System.Linq", "Telegram.Bot.Types");

    private readonly ConcurrentDictionary<string, ScriptRunner<bool>?> _runners = new();

    public bool Evaluate(UserTriggeredResponse response, Message message)
    {
        if (string.IsNullOrWhiteSpace(response.Condition)) return false;

        var runner = _runners.GetOrAdd(response.Condition, Compile, response.Name);

        if (runner is null) return false;

        try
        {
            // Compiled snippets contain no awaits, so the task completes synchronously.
            return runner(new UserTriggeredConditionGlobals { message = message }).GetAwaiter().GetResult();
        }
        catch (Exception exception)
        {
            logger.LogWarning(exception, "User-triggered condition {Name} threw; treating as no match", response.Name);
            return false;
        }
    }

    private ScriptRunner<bool>? Compile(string condition, string name)
    {
        try
        {
            var runner = CSharpScript.Create<bool>(condition, Options, typeof(UserTriggeredConditionGlobals)).CreateDelegate();

            logger.LogInformation("Compiled user-triggered condition {Name}", name);

            return runner;
        }
        catch (CompilationErrorException exception)
        {
            logger.LogError(exception, "User-triggered condition {Name} failed to compile; trigger disabled", name);

            return null;
        }
    }
}

public class UserTriggeredConditionGlobals
{
    // Lowercase on purpose: scripts read as `message.Text != null && message.Text.Length >= 200`.
    // ReSharper disable once InconsistentNaming
    public required Message message { get; init; }
}
