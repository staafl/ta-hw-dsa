using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    /* 1 Write a program that counts in a given array of double values the number of occurrences of each value. Use Dictionary<TKey,TValue>.
    Example: array = {3, 4, 4, -2.5, 3, 3, 4, 3, -2.5}
    -2.5  2 times
    3  4 times
    4  3 times
    * */
    static void Main(string[] args)
    {
        var array = new[] { 3, 4, 4, -2.5, 3, 3, 4, 3, -2.5 };

        var counter = new Dictionary<double, int>();

        foreach (double num in array)
        {
            int previous;
            counter[num] = counter.TryGetValue(num, out previous) ? 1 + previous : 1;
        }

        foreach (var kvp in counter)
            Console.WriteLine("{0}: {1} times", kvp.Key, kvp.Value);
    }
}