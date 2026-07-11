using System.Threading.Channels;

namespace FOSCBot.Infrastructure.Intelligence.Services;

public sealed class UserMemoryChannel
{
    public Channel<MemoryBatch> Channel { get; } =
        System.Threading.Channels.Channel.CreateBounded<MemoryBatch>(new BoundedChannelOptions(64)
        {
            FullMode = BoundedChannelFullMode.DropOldest,
            SingleReader = true,
            SingleWriter = false
        });
}

public sealed record MemoryBatch(long ChatId, List<AccumulatedMessage> Messages);

public sealed record AccumulatedMessage(long UserId, string Username, string Text, DateTimeOffset Timestamp);
