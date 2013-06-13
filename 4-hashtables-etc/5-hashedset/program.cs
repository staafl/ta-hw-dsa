using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
class Program
{
    /* 5 Implement the data structure "set" in a class HashedSet<T> using your class HashTable<K,T> 
     * to hold the elements. Implement all standard set operations like Add(T), Find(T), Remove(T),
     * Count, Clear(), union and intersect.
     * */
    static void Main(string[] args)
    {
        var set = new HashedSet<int>();

        Debug.Assert(set.Count == 0);
        Debug.Assert(!set.Find(1));

        set.Add(1);

        Debug.Assert(set.Count == 1);
        Debug.Assert(set.Find(1));

        set.Add(2);

        Debug.Assert(set.Count == 2);
        Debug.Assert(set.Find(2));

        set.Add(1);

        Debug.Assert(set.Count == 2);
        Debug.Assert(set.Find(1));

        set.Remove(1);

        Debug.Assert(set.Count == 1);
        Debug.Assert(!set.Find(1));
        Debug.Assert(set.Find(2));

        var set1 = new HashedSet<int> { 1, 2, 3, 4, 5, 6 }.Intersect(new HashedSet<int> { 2, 4, 6, 8, 10 });
        Debug.Assert(set1.SameContents(new[] { 2, 4, 6 }, i => i));

        var set2 = new HashedSet<int> { 1, 2, 3, 4, 5, 6 }.Union(new HashedSet<int> { 2, 4, 6, 8, 10 });
        Debug.Assert(set2.SameContents(new[] { 1, 2, 3, 4, 5, 6, 8, 10 }, i => i));
    }
}