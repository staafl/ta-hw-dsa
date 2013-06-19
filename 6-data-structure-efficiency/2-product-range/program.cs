using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Wintellect.PowerCollections;
using System.IO;
class Program
{
    /* 2. A large trade company has millions of articles, each described 
       by barcode, vendor, title and price. Implement a data structure to 
       store them that allows fast retrieval of all articles in given price 
       range [x...y]. 
       
       Hint: use OrderedMultiDictionary<K,T> from Wintellect's Power 
       Collections for .NET. 
     * */

    static void Main(string[] args)
    {
    
        // What's there to "implement"?
        // That's exactly what OrderedMultiDictionary was made for.
        
        // input generator in bin\debug\generator.cs

        var products = new OrderedMultiDictionary<decimal, string>(true);

        foreach (var line in File.ReadLines("products.txt").Skip(1))
        {
            var split = line.Split('|');
            products.Add(decimal.Parse(split[0]), split[1]);
        }

        foreach (var line in File.ReadLines("commands.txt"))
        {
            Console.WriteLine("Command: " + line);
            var split = line.Split('|');

            var first = decimal.Parse(split[0]);
            var second = decimal.Parse(split[1]);

            var matching = products.Range(Math.Min(first, second), true,
                                          Math.Max(first, second), true);

            Console.WriteLine(string.Join(", ", matching));
            Console.WriteLine();
            Console.WriteLine("Press Ctrl+C to quit, or any other key to continue...");
            Console.ReadLine();
        }


    }
}