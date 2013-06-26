using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 0 || Debugger.IsAttached)
        {
            Console.SetIn(new StringReader(
@"3
2 3
2 2
3 2"));
        }

        var elems = new List<Tuple<int, string>>();

        int count = int.Parse(Console.ReadLine());
        for (int ii = 0; ii < count; ++ii)
        {
            var pair = Console.ReadLine().Split(' ');

            elems.Add(Tuple.Create(ii, "(" + pair[0] + ", " + pair[1] + ")"));
            if (pair[0] != pair[1])
                elems.Add(Tuple.Create(ii, "(" + pair[1] + ", " + pair[0] + ")"));
        }

        var sb = new StringBuilder();
        var generated = new List<string>();

        Recurse(elems,
                new HashSet<int>(), count, 0, sb, generated);

        Console.WriteLine(generated.Count);
        generated.Sort();
        foreach (var str in generated)
        {
            Console.WriteLine(str);
        }

        if (Debugger.IsAttached)
        {
            Console.ReadKey();
        }
    }


    static void Recurse(IEnumerable<Tuple<int, string>> indexedElements,
                        HashSet<int> seenEver, int totalCount,  int count, StringBuilder sofar, List<string> generated)
    {
        var seenNow = new HashSet<string>();

        if (count == totalCount)
        {
            generated.Add(sofar.ToString());
            return;
        }

        foreach (var tuple in indexedElements)
        {
            if (seenEver.Contains(tuple.Item1))
                continue;

            if (seenNow.Contains(tuple.Item2))
                continue;

            seenEver.Add(tuple.Item1);
            seenNow.Add(tuple.Item2);

            var lenBefore = sofar.Length;

            if (lenBefore != 0)
                sofar.Append(" | ");
            sofar.Append(tuple.Item2);

            Recurse(indexedElements, seenEver,totalCount, count + 1, sofar, generated);

            sofar.Length = lenBefore;

            seenEver.Remove(tuple.Item1);

        }
    }
}