using System.Threading.Channels;
using FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Modules.SocialCredit.Infrastructure;

public class MessageQueueService : IMessageQueueService
{
    private readonly Channel<Message> _messageChannel;
    private readonly ChannelWriter<Message> _writer;
    private readonly ChannelReader<Message> _reader;
    private readonly ILogger<MessageQueueService> _logger;

    public MessageQueueService(ILogger<MessageQueueService> logger, IOptions<MessageQueueServiceOptions> options)
    {
        _logger = logger;

        // Create a bounded channel with capacity of 1000 messages
        var channelOptions = new BoundedChannelOptions(options.Value.MaxQueueSize)
        {
            FullMode = BoundedChannelFullMode.Wait, 
            SingleReader = false,
            SingleWriter = false 
        };

        _messageChannel = Channel.CreateBounded<Message>(channelOptions);
        _writer = _messageChannel.Writer;
        _reader = _messageChannel.Reader;
    }

    public async Task EnqueueMessageAsync(Message message, CancellationToken cancellationToken = default)
    {
        try
        {
            await _writer.WriteAsync(message, cancellationToken);
            _logger.LogDebug("Enqueued message {MessageId} from user {UserId} in chat {ChatId}",
                message.MessageId, message.From?.Id, message.Chat.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to enqueue message {MessageId}", message.MessageId);
            throw;
        }
    }

    public async Task<Message?> DequeueMessageAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (await _reader.WaitToReadAsync(cancellationToken))
            {
                if (_reader.TryRead(out var message))
                {
                    _logger.LogDebug("Dequeued message {MessageId} from user {UserId} in chat {ChatId}",
                        message.MessageId, message.From?.Id, message.Chat.Id);
                    return message;
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to dequeue message");
            return null;
        }
    }
}

public class MessageQueueServiceOptions
{
    public int MaxQueueSize { get; set; } = 10000;
}