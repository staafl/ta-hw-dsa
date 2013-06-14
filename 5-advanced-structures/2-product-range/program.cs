using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Wintellect.PowerCollections;
using System.IO;
class Program
{
    /* 2 Write a program to read a large collection of products 
     * (name + price) and efficiently find the first 20 products 
     * in the price range [a...b]. Test for 500 000 products and 
     * 10 000 price searches.
     * Hint: you may use OrderedBag<T> and sub-ranges.
     * */

    static void Main(string[] args)
    {
        // input generator in bin\debug\generator.cs

        var products = new OrderedMultiDictionary<decimal, string>(true);

        foreach (var line in File.ReadLines("products.txt"))
        {
            var split = line.Split('|');
            products.Add(decimal.Parse(split[1]), split[0]);
        }

        int totalCount = 0;

        // show one line of results and exit OR benchmark all 10,000 commands
        // without printing anything
        const bool JUST_ONE = true;

        var sw = new Stopwatch();
        sw.Start();

        int queries = 0;
        foreach (var line in File.ReadLines("commands.txt"))
        {
            queries += 1;
            var split = line.Split('|');

            var first = decimal.Parse(split[0]);
            var second = decimal.Parse(split[1]);

            var first20 = products.Range(Math.Min(first, second), true,
                                         Math.Max(first, second), true)
                                  .Take(20);

            totalCount += first20.Count();

            if (JUST_ONE)
            {
                Console.WriteLine(line);
                Console.WriteLine(String.Join(",", first20.Select(kvp => "{" + kvp.Key + ":" + string.Join(",", kvp.Value) + "}")));
                Console.WriteLine();
                break;
            }
        }

        if (JUST_ONE)
            return;

        sw.Stop();

        Debug.Assert(totalCount <= queries * 20 && queries * 20 - totalCount < 100);
        Console.WriteLine("Total time: " + sw.Elapsed.TotalMilliseconds);

    }
}