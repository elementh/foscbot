using System.Text.Json.Serialization;

namespace FOSCBot.Infrastructure.Contract.Model
{
    public class YesNoAnswerModel
    {
        [JsonPropertyName("answer")]
        public string Answer { get; set; }
        [JsonPropertyName("forced")]
        public bool Forced { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}