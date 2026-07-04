namespace FOSCBot.Core.Module.Options;

public class UserTriggeredResponseOptions
{
    public const string Key = "UserTriggeredResponses";

    public UserTriggeredResponse[] Responses { get; set; } = [];
}

public class UserTriggeredResponse
{
    public string Name { get; set; } = string.Empty;

    public long? UserId { get; set; }

    public string? Username { get; set; }

    /// <summary>
    /// C# snippet evaluated against the incoming message, with <c>message</c> in scope,
    /// e.g. <c>message.Text != null &amp;&amp; message.Text.Length >= 200</c>.
    /// Statements with explicit <c>return</c> work too.
    /// </summary>
    public string Condition { get; set; } = string.Empty;

    public string[] Phrases { get; set; } = [];
}
