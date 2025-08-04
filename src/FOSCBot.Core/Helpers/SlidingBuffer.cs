using System.Collections;

namespace FOSCBot.Core.Helpers;

// Courtesy of Thomas Levesque
public class SlidingBuffer<T> : IEnumerable<T>
{
    private readonly Queue<T> _queue;

    public SlidingBuffer(int maxCount)
    {
        MaxLength = maxCount;
        _queue = new Queue<T>(maxCount);
    }

    public int MaxLength { get; }

    public IEnumerator<T> GetEnumerator()
    {
        return _queue.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        if (_queue.Count == MaxLength)
            _queue.Dequeue();
        _queue.Enqueue(item);
    }
}
