using System;
using System.Collections.Generic;
using System.Threading;

namespace FOSCBot.Common.Helper;

/// <summary>
/// Random provider thanks to https://csharpindepth.com/Articles/Random
/// </summary>
public static class RandomProvider
{    
    private static int _seed = Environment.TickCount;

    private static readonly ThreadLocal<Random> RandomWrapper = new(() => new Random(Interlocked.Increment(ref _seed)));

    public static Random GetThreadRandom()
    {
        return RandomWrapper.Value;
    }

    public static T GetRandomFromList<T>(this List<T> list) where T : class
    {
        var position = GetThreadRandom().Next(0, list.Count);
        return list[position];
    }
}