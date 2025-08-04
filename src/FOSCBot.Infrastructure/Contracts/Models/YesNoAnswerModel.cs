using System.Text.Json.Serialization;

namespace FOSCBot.Infrastructure.Contracts.Models;

public class YesNoAnswerModel
{
    [JsonPropertyName("answer")]
    public string Answer { get; set; }
    [JsonPropertyName("forced")]
    public bool Forced { get; set; }
    [JsonPropertyName("image")]
    public string Image { get; set; }
}