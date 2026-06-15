using System.Text.Json;
using System.Text.Json.Serialization;

namespace FOSCBot.Core.Module.Models;

public record PhantomCommandDefinition(
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("outputFormat")] string OutputFormat,
    [property: JsonPropertyName("responseLength")] string ResponseLength,
    [property: JsonPropertyName("tone")] string Tone,
    [property: JsonPropertyName("examples")] PhantomCommandExample[] Examples,
    [property: JsonPropertyName("constraints")] string[] Constraints)
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static PhantomCommandDefinition? TryParse(string json)
    {
        try
        {
            var definition = JsonSerializer.Deserialize<PhantomCommandDefinition>(json, SerializerOptions);

            if (definition is null
                || string.IsNullOrWhiteSpace(definition.Description)
                || string.IsNullOrWhiteSpace(definition.OutputFormat)
                || string.IsNullOrWhiteSpace(definition.ResponseLength)
                || string.IsNullOrWhiteSpace(definition.Tone)
                || definition.Examples is not { Length: > 0 }
                || definition.Constraints is not { Length: > 0 })
                return null;

            return definition;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    public string Serialize() => JsonSerializer.Serialize(this, SerializerOptions);

    public string FormatExamples()
    {
        if (Examples.Length == 0)
            return "No examples available.";

        return string.Join("\n", Examples.Select((ex, i) =>
            $"- Input: {ex.Arguments ?? "no arguments"} -> Output: {ex.Response}"));
    }

    public string FormatConstraints() =>
        string.Join("\n", Constraints.Select(c => $"- {c}"));
}

public record PhantomCommandExample(
    [property: JsonPropertyName("arguments")] string? Arguments,
    [property: JsonPropertyName("response")] string Response);
