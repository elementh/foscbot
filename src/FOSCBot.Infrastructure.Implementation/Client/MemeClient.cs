using System.Net.Http.Json;
using System.Text.Json.Serialization;
using FOSCBot.Infrastructure.Contract.Client;

namespace FOSCBot.Infrastructure.Implementation.Client;

public class MemeClient : IMemeClient
{
    protected readonly HttpClient Client;

    public MemeClient(HttpClient client)
    {
        Client = client;
    }

    public async Task<string> GetSentence(int quantity, CancellationToken cancellationToken = default)
    {
        var response = await Client.GetAsync($"sentences/{quantity}", cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
    
    public async Task<Uri?> GenerateMeme(string text, CancellationToken cancellationToken)
    {
        var session = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var post = await Client.GetFromJsonAsync<CreatePostModel>(
            $"https://brain.predis.ai/tools/create_post/?session_id={session}&post_idea={Uri.EscapeDataString(text)}");

        return post is not null
            ? new Uri(
                $"https://brain.predis.ai/users/download_creative/?session_id={session}&media_type=single_image&event_id={post.Data.First().EventId}")
            : default;
    }

    public async Task<bool> CheckIfDone(Uri uri, CancellationToken cancellationToken)
    {
        var response = await Client.GetStreamAsync(uri, cancellationToken);

        return !response.GetType().ToString().Contains("EmptyReadStream");
    }

    public async Task<Stream?> Download(Uri uri, CancellationToken cancellationToken)
    {
        return await Client.GetStreamAsync(uri, cancellationToken);
    }

    protected class CreatePostModel
    {
        [JsonPropertyName("data")] public DataModel[] Data { get; set; } = null!;

        public class DataModel
        {
            [JsonPropertyName("event_id")] public string EventId { get; set; } = null!;
        }
    }
}