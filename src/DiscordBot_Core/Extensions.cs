using System;

public static class Extensions
{
    private static readonly Random _rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        var n = list.Count;
        for (var i = n - 1; i > 0; i--)
        {
            var j = _rng.Next(i + 1); // random index from 0 to i
            (list[i], list[j]) = (list[j], list[i]); // swap
        }
    }
}