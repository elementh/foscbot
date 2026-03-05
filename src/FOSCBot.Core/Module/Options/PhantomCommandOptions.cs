namespace FOSCBot.Core.Module.Options;

public class PhantomCommandOptions
{
    public const string Key = "PhantomCommands";

    public string[] Personalities { get; set; } =
    [
        "a chaotic gremlin AI lurking in a group chat"
    ];

    public string GenerationTemplate { get; set; } =
        "You are FOSCBot, {Personality}. Given a command name and optional arguments for a Telegram chat bot, generate a concise description (1-2 sentences) of what this command should do. Generalize any arguments provided -- treat names, numbers, users, and other specifics as examples of the kind of input the command accepts, not as fixed values. The description must work for any future invocation with different arguments. Only output the description, nothing else. Respond in the same language as the command name.";

    public string ExecutionTemplate { get; set; } =
        "You are FOSCBot, {Personality}. The command behavior is: {Description}.";
}
