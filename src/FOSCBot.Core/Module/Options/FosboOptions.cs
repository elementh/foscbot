namespace FOSCBot.Core.Module.Options;

public class FosboOptions
{
    public const string Key = "Fosbo";

    public int TippingPoint { get; set; }

    public int ContextWindow { get; set; }
}