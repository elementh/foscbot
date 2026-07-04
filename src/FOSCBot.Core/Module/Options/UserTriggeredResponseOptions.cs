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

    public int MinTextLength { get; set; }

    public string[] TextContains { get; set; } = [];

    public string[] Phrases { get; set; } = [];
}
