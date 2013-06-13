using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
class Program
{
    /* 4 Implement the data structure "hash table" in a class HashTable<K,T>. Keep the data in array of 
     * lists of key-value pairs (LinkedList<KeyValuePair<K,T>>[]) with initial capacity of 16. When the 
     * hash table load runs over 75%, perform resizing to 2 times larger capacity. Implement the following 
     * methods and properties: Add(key, value), Find(key)value, Remove( key), Count, Clear(), this[], Keys. 
     * Try to make the hash table to support iterating over its elements with foreach.
     * */
    static void Main(string[] args)
    {
        var ht = new HashTable<string, int>();

        foreach (var word in new[] { "aaa", "aaa", "bbb", "ccc", "aaa", "bbb" })
            ht.Add(word, 1 + ht.GetOrDefault(word, 0));

        Debug.Assert(ht["aaa"] == 3);
        Debug.Assert(ht["bbb"] == 2);
        Debug.Assert(ht["ccc"] == 1);

        var ht2 = new HashTable<MockHash, string>(4);

        Debug.Assert(ht2.Count == 0);
        Debug.Assert(ht2.OccupiedBuckets == 0);
        Debug.Assert(ht2.Capacity == 4);

        ht2[new MockHash(0, 0)] = "00";

        Debug.Assert(ht2.Count == 1);
        Debug.Assert(ht2.OccupiedBuckets == 1);
        Debug.Assert(ht2.Capacity == 4);

        ht2[new MockHash(0, 1)] = "01";

        Debug.Assert(ht2.Count == 2);
        Debug.Assert(ht2.OccupiedBuckets == 1);
        Debug.Assert(ht2.Capacity == 4);

        ht2[new MockHash(1, 2)] = "12";

        Debug.Assert(ht2.Count == 3);
        Debug.Assert(ht2.OccupiedBuckets == 2);
        Debug.Assert(ht2.Capacity == 8);

        ht2[new MockHash(1, 3)] = "13";

        Debug.Assert(ht2.Count == 4);
        Debug.Assert(ht2.OccupiedBuckets == 2);
        Debug.Assert(ht2.Capacity == 8);

        Debug.Assert(ht2.Remove(new MockHash(1, 3)));
        Debug.Assert(!ht2.Remove(new MockHash(1, 3)));

        Debug.Assert(ht2.Count == 3);
        Debug.Assert(ht2.OccupiedBuckets == 2);
        Debug.Assert(ht2.Capacity == 8);

        Debug.Assert(ht2.Remove(new MockHash(1, 2)));
        Debug.Assert(!ht2.Remove(new MockHash(1, 2)));

        Debug.Assert(ht2.Count == 2);
        Debug.Assert(ht2.OccupiedBuckets == 1);
        Debug.Assert(ht2.Capacity == 8);


    }


}
