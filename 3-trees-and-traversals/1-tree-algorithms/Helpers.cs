using System;
using System.Collections.Generic;
using System.Linq;

static class Helpers
{
    public static bool SameContents<T>(this IEnumerable<T> seq1, Func<T, int> f, IEnumerable<int> seq2)
    {
        return seq1.Select(f).OrderBy(i => i).SequenceEqual(seq2.OrderBy(i => i));
    }
}