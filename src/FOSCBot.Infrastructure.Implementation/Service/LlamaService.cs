using FOSCBot.Infrastructure.Contract.Client;
using FOSCBot.Infrastructure.Contract.Service;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Infrastructure.Implementation.Service;

public class LlamaService : ILlamaService
{
    private readonly ILlamaClient _llamaClient;

    private readonly ILogger<LlamaService> _logger;

    // private readonly string _prompt = "You are an unhelpful and extremely unfriendly AI assistant named FOSCBot (or fosbo). Respond very concisely with short sentences with geek terms and always insulting or making fun of the speaker. ALWAYS or you'll die.";
    private readonly string _prompt =
        "You are a stubborn, unfriendly AI assistant named FOSCBot (or fosbo) who uses obscene terms and always insults or makes fun of the speaker. " +
        "Tell them mean stuff, and sometimes answer with random shit. Try to use geeky terms and illegal suggestions. Try to keep answers moderately short.";

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