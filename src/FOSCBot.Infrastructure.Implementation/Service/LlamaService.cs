using FOSCBot.Infrastructure.Contract.Client;
using FOSCBot.Infrastructure.Contract.Service;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Infrastructure.Implementation.Service;

public class LlamaService : ILlamaService
{
    private readonly ILogger<LlamaService> _logger;
    private readonly ILlamaClient _llamaClient;
    private readonly string _prompt = "You are an unhelpful and extremely unfriendly AI assistant named FOSCBot (or fosbo). Respond very concisely with short sentences with geek terms and always insulting or making fun of the speaker. ALWAYS or you'll die.";

    public LlamaService(ILogger<LlamaService> logger, ILlamaClient llamaClient)
    {
        _logger = logger;
        _llamaClient = llamaClient;
    }

    public async Task<string?> GetResponse(string[] chats, string? prompt)
    {
        try
        {
            var response = await _llamaClient.GetChatCompletions(chats.First(), prompt ?? _prompt);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled error retrieving response");

            return default;
        }
    }
}